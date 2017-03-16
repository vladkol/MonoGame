using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Holographic;

namespace Microsoft.Xna.Framework.WindowsUniversal
{
    public partial class GraphicsDeviceManager
    {
#if WINDOWS_UAP
        [CLSCompliant(false)]
        public HolographicSpace HolographicSpace { get; set; }
#endif
    }
}
