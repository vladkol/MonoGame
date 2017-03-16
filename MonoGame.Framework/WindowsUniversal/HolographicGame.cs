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
    /// Static class for initializing a Game object for a Windows Mixed Reality application.
    /// </summary>
    /// <typeparam name="T">A class derived from Game.</typeparam>
    [CLSCompliant(false)]
    public class HolographicGame<T>
        where T : Game, new()
    {
        /// <summary>
        /// Creates your Game class initializing it to work within a XAML application window.
        /// </summary>
        /// <param name="launchParameters">The command line arguments from launch.</param>
        /// <param name="window">The core window object.</param>
        /// <returns></returns>
        static public T Create(string launchParameters, CoreWindow window)
        {
            if (window == null)
                throw new NullReferenceException("The window cannot be null!");

            // Save any launch parameters to be parsed by the platform.
            HolographicGamePlatform.LaunchParameters = launchParameters != null ? launchParameters : string.Empty;

            // Setup the window class.
            HolographicGameWindow.Instance.Initialize(window);

            // Construct the game.
            var game = new T();

            // Set the swap chain panel on the graphics mananger.
            if (game.graphicsDeviceManager == null)
                throw new NullReferenceException("You must create the GraphicsDeviceManager in the Game constructor!");

            game.graphicsDeviceManager.PreparingDeviceSettings += (object _sender, PreparingDeviceSettingsEventArgs _e) =>
            {
                //_e.GraphicsDeviceInformation.Adapter;
                // TODO: select a holographic adapter by LUID 
            };

            game.graphicsDeviceManager.ApplyChanges();

            // Start running the game.
            game.Run(GameRunBehavior.Asynchronous);

            // Return the created game object.
            return game;
        }   

        
    }
}
