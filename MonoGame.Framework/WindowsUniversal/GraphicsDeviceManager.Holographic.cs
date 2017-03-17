using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Holographic;
using Windows.Perception.Spatial;

namespace Microsoft.Xna.Framework
{
    public partial class GraphicsDeviceManager
    {
#if WINDOWS_UAP
        [CLSCompliant(false)]
        public HolographicSpace HolographicSpace { get; internal set; }

        [CLSCompliant(false)]
        public SpatialStationaryFrameOfReference SpatialReferenceFrame { get; internal set; }

        [CLSCompliant(false)]
        public HolographicFrame HolographicFrame { get; internal set; }

        [CLSCompliant(false)]
        public HolographicCameraPose HolographicCameraPose { get; internal set; }

        [CLSCompliant(false)]
        public HolographicStereoTransform HolographicStereoTransform { get; internal set; }

        [CLSCompliant(false)]
        public System.Numerics.Vector3 HolographicFocusPoint { get; set; }
#endif
    }
}
