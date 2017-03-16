using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.WindowsUniversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <param name="isHolographic">Use Windows Holographic device</param>
        /// <returns></returns>
        static public T Create(string launchParameters, CoreWindow window, bool isHolographic)
        {
            if (window == null)
                throw new NullReferenceException("The window cannot be null!");

            // Save any launch parameters to be parsed by the platform.
            UAPGamePlatform.LaunchParameters = launchParameters != null ? launchParameters : string.Empty;

            // Setup the window class.
            UAPGameWindow.Instance.Initialize(window, UAPGamePlatform.TouchQueue, isHolographic);

            // Construct the game.
            var game = new T();
            game.IsFixedTimeStep = false;

            // Set the swap chain panel on the graphics mananger.
            if (game.graphicsDeviceManager == null)
                throw new NullReferenceException("You must create the GraphicsDeviceManager in the Game constructor!");

            game.graphicsDeviceManager.PreparingDeviceSettings += (object _sender, PreparingDeviceSettingsEventArgs _e) =>
            {
                if(game.LaunchParameters.ContainsKey("HolographicAdapter"))
                {
                    ulong adapterLUID = ulong.Parse(game.LaunchParameters["HolographicAdapter"]);
                    _e.GraphicsDeviceInformation.Adapter = Microsoft.Xna.Framework.Graphics.GraphicsAdapter.Adapters.Where(adapter => adapter.LUID == adapterLUID).First();
                }
                _e.GraphicsDeviceInformation.GraphicsProfile = Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef;

            };

            game.graphicsDeviceManager.ApplyChanges();

            // Start running the game.
            game.Run(GameRunBehavior.Asynchronous);

            // Return the created game object.
            return game;
        }   

        
    }
}
