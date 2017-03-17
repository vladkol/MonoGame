using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.WindowsUniversal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Holographic;
using Windows.UI.Core;

namespace MonoGame.Framework
{
    /// <summary>
    /// Static class for initializing a Game object for a Universal Windows Platform non-XAML application.
    /// </summary>
    /// <typeparam name="T">A class derived from Game.</typeparam>
    [CLSCompliant(false)]
    public class UWPGame<T>
        where T : Game, new()
    {
        /// <summary>
        /// Creates your Game class initializing it to work within a XAML application window.
        /// </summary>
        /// <param name="launchParameters">The command line arguments from launch.</param>
        /// <param name="window">The core window object.</param>
        /// <param name="holographicSpace">Windows Holographic space if using</param>
        /// <returns></returns>
        static public T Create(string launchParameters, CoreWindow window, HolographicSpace holographicSpace)
        {
            if (window == null)
                throw new NullReferenceException("The window cannot be null!");

            // Save any launch parameters to be parsed by the platform.
            UAPGamePlatform.LaunchParameters = launchParameters != null ? launchParameters : string.Empty;

            // Setup the window class.
            UAPGameWindow.Instance.Initialize(window, UAPGamePlatform.TouchQueue, holographicSpace != null);

            // Construct the game.
            var game = new T();
            game.IsFixedTimeStep = false;

            // Set the swap chain panel on the graphics mananger.
            if (game.graphicsDeviceManager == null)
                throw new NullReferenceException("You must create the GraphicsDeviceManager in the Game constructor!");

            game.graphicsDeviceManager.HolographicSpace = holographicSpace;
            game.graphicsDeviceManager.GraphicsProfile = Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef;

            game.graphicsDeviceManager.PreparingDeviceSettings += (object _sender, PreparingDeviceSettingsEventArgs _e) =>
            {
                if (holographicSpace != null)
                {
                    int shiftPos = sizeof(uint);
                    ulong adapterLUID = (ulong)holographicSpace.PrimaryAdapterId.LowPart | (((ulong)holographicSpace.PrimaryAdapterId.HighPart) << shiftPos);
                    _e.GraphicsDeviceInformation.Adapter = Microsoft.Xna.Framework.Graphics.GraphicsAdapter.Adapters.Where(adapter => adapter.LUID == adapterLUID).First();
                }
            };

            //game.graphicsDeviceManager.ApplyChanges();

            // Start running the game.
            //game.Run(GameRunBehavior.Asynchronous);

            // Return the created game object.
            return game;
        }   

        
    }
}
