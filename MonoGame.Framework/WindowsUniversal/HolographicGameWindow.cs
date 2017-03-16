using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

namespace Microsoft.Xna.Framework.WindowsUniversal
{
    partial class HolographicGameWindow : GameWindow
    {
        private CoreWindow _coreWindow;
        private DisplayInformation _dinfo;
        private ApplicationView _appView;
        private Rectangle _viewBounds;

        private object _eventLocker = new object();

        private bool _isFocusChanged = false;
        private CoreWindowActivationState _newActivationState;

        #region Internal Properties

        internal Game Game { get; set; }

        public ApplicationView AppView { get { return _appView; } }

        internal bool IsExiting { get; set; }

        #endregion

        #region Public Properties

        public override IntPtr Handle { get { return Marshal.GetIUnknownForObject(_coreWindow); } }

        public override string ScreenDeviceName { get { return String.Empty; } } // window.Title

        public override Rectangle ClientBounds { get { return _viewBounds; } }

        public override bool AllowUserResizing
        {
            get { return false; }
            set
            {
                // You cannot resize a Metro window!
            }
        }

        public override DisplayOrientation CurrentOrientation
        {
            get { return DisplayOrientation.LandscapeLeft; }
        }

        private UAPGamePlatform Platform { get { return Game.Instance.Platform as UAPGamePlatform; } }

        protected internal override void SetSupportedOrientations(DisplayOrientation orientations)
        {

        }

        #endregion

        static public HolographicGameWindow Instance { get; private set; }

        static HolographicGameWindow()
        {
            Instance = new HolographicGameWindow();
        }

        public void Initialize(CoreWindow coreWindow)
        {
            _coreWindow = coreWindow;

            _dinfo = DisplayInformation.GetForCurrentView();
            _appView = ApplicationView.GetForCurrentView();

            _coreWindow.Closed += Window_Closed;
            _coreWindow.Activated += Window_FocusChanged;

            SetViewBounds(_appView.VisibleBounds.Width, _appView.VisibleBounds.Height);

            SetCursor(false);
        }

        private void Window_FocusChanged(CoreWindow sender, WindowActivatedEventArgs args)
        {
            lock (_eventLocker)
            {
                _isFocusChanged = true;
                _newActivationState = args.WindowActivationState;
            }
        }

        private void UpdateFocus()
        {
            lock (_eventLocker)
            {
                _isFocusChanged = false;

                if (_newActivationState == CoreWindowActivationState.Deactivated)
                    Platform.IsActive = false;
                else
                    Platform.IsActive = true;
            }
        }

        private void Window_Closed(CoreWindow sender, CoreWindowEventArgs args)
        {
            Game.SuppressDraw();
            Game.Platform.Exit();
        }

        private void SetViewBounds(double width, double height)
        {
            var pixelWidth = Math.Max(1, (int)Math.Round(width * _dinfo.RawPixelsPerViewPixel));
            var pixelHeight = Math.Max(1, (int)Math.Round(height * _dinfo.RawPixelsPerViewPixel));
            _viewBounds = new Rectangle(0, 0, pixelWidth, pixelHeight);
        }


        protected override void SetTitle(string title)
        {
            Debug.WriteLine("WARNING: GameWindow.Title has no effect under UWP.");
        }

        internal void SetCursor(bool visible)
        {
            if (_coreWindow == null)
                return;

            var asyncResult = _coreWindow.Dispatcher.RunIdleAsync((e) =>
            {
                if (visible)
                    _coreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 0);
                else
                    _coreWindow.PointerCursor = null;
            });
        }

        internal void RunLoop()
        {
            SetCursor(Game.IsMouseVisible);
            _coreWindow.Activate();

            while (true)
            {
                if (Platform.IsActive)
                {
                    // Process events incoming to the window.
                    _coreWindow.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessAllIfPresent);

                    Tick();
                }
                else
                {
                    _coreWindow.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessOneAndAllPending);
                }
                if (IsExiting)
                    break;
            }
        }

        void ProcessWindowEvents()
        {

        }

        internal void Tick()
        {
            // Update state based on window events.
            ProcessWindowEvents();

            // Update and render the game.
            if (Game != null)
                Game.Tick();
        }

        #region Public Methods

        public void Dispose()
        {
            //window.Dispose();
        }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
        }

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {

        }

        #endregion
    }
}

namespace Microsoft.Xna.Framework.Graphics
{
    partial class GraphicsAdapter
    {
        [CLSCompliant(false)]
        public ulong LUID
        {
            get
            {
                ulong luid = 0;
                IntPtr adapterPtr = IntPtr.Zero;
                _adapter.QueryInterface(typeof(Microsoft.Xna.Framework.WindowsUniversal.IDxgiAdapter).GetTypeInfo().GUID, out adapterPtr);
                if(adapterPtr != IntPtr.Zero)
                {
                    luid = (Marshal.GetObjectForIUnknown(adapterPtr) as Microsoft.Xna.Framework.WindowsUniversal.IDxgiAdapter).GetDesc().AdapterLuid;
                }
                return luid;
            }
        }
    }
}

