using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.WindowsUniversal
{
    /// <summary>
    /// Describes an adapter (or video card) by using DXGI 1.0.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct DxgiAdapterDesc : IEquatable<DxgiAdapterDesc>
    {
        /// <summary>
        /// A string that contains the adapter description. On feature level 9 graphics hardware, “Software Adapter”.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        private string adapterDescription;

        /// <summary>
        /// The PCI ID of the hardware vendor. On feature level 9 graphics hardware, 0.
        /// </summary>
        private uint vendorId;

        /// <summary>
        /// The PCI ID of the hardware device. On feature level 9 graphics hardware, 0.
        /// </summary>
        private uint deviceId;

        /// <summary>
        /// The PCI ID of the sub system. On feature level 9 graphics hardware, 0.
        /// </summary>
        private uint subSysId;

        /// <summary>
        /// The PCI ID of the revision number of the adapter. On feature level 9 graphics hardware, 0.
        /// </summary>
        private uint revision;

        /// <summary>
        /// The number of bytes of dedicated video memory that are not shared with the CPU.
        /// </summary>
        private UIntPtr dedicatedVideoMemory;

        /// <summary>
        /// The number of bytes of dedicated system memory that are not shared with the CPU. This memory is allocated from available system memory at boot time.
        /// </summary>
        private UIntPtr dedicatedSystemMemory;

        /// <summary>
        /// The number of bytes of shared system memory. This is the maximum value of system memory that may be consumed by the adapter during operation. Any incidental memory consumed by the driver as it manages and uses video memory is additional.
        /// </summary>
        private UIntPtr sharedSystemMemory;

        /// <summary>
        /// A unique value that identifies the adapter.
        /// </summary>
        private ulong adapterLuid;

        /// <summary>
        /// Gets a string that contains the adapter description. On feature level 9 graphics hardware, “Software Adapter”.
        /// </summary>
        public string AdapterDescription
        {
            get { return this.adapterDescription; }
        }

        /// <summary>
        /// Gets the PCI ID of the hardware vendor. On feature level 9 graphics hardware, 0.
        /// </summary>
        public uint VendorId
        {
            get { return this.vendorId; }
        }

        /// <summary>
        /// Gets the PCI ID of the hardware device. On feature level 9 graphics hardware, 0.
        /// </summary>
        public uint DeviceId
        {
            get { return this.deviceId; }
        }

        /// <summary>
        /// Gets the PCI ID of the sub system. On feature level 9 graphics hardware, 0.
        /// </summary>
        public uint SubSysId
        {
            get { return this.subSysId; }
        }

        /// <summary>
        /// Gets the PCI ID of the revision number of the adapter. On feature level 9 graphics hardware, 0.
        /// </summary>
        public uint Revision
        {
            get { return this.revision; }
        }

        /// <summary>
        /// Gets the number of bytes of dedicated video memory that are not shared with the CPU.
        /// </summary>
        public ulong DedicatedVideoMemory
        {
            get { return this.dedicatedVideoMemory.ToUInt64(); }
        }

        /// <summary>
        /// Gets the number of bytes of dedicated system memory that are not shared with the CPU. This memory is allocated from available system memory at boot time.
        /// </summary>
        public ulong DedicatedSystemMemory
        {
            get { return this.dedicatedSystemMemory.ToUInt64(); }
        }

        /// <summary>
        /// Gets the number of bytes of shared system memory. This is the maximum value of system memory that may be consumed by the adapter during operation. Any incidental memory consumed by the driver as it manages and uses video memory is additional.
        /// </summary>
        public ulong SharedSystemMemory
        {
            get { return this.sharedSystemMemory.ToUInt64(); }
        }

        /// <summary>
        /// Gets a unique value that identifies the adapter.
        /// </summary>
        public ulong AdapterLuid
        {
            get { return this.adapterLuid; }
        }

        /// <summary>
        /// Compares two <see cref="DxgiAdapterDesc"/> objects. The result specifies whether the values of the two objects are equal.
        /// </summary>
        /// <param name="left">The left <see cref="DxgiAdapterDesc"/> to compare.</param>
        /// <param name="right">The right <see cref="DxgiAdapterDesc"/> to compare.</param>
        /// <returns><value>true</value> if the values of left and right are equal; otherwise, <value>false</value>.</returns>
        public static bool operator ==(DxgiAdapterDesc left, DxgiAdapterDesc right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="DxgiAdapterDesc"/> objects. The result specifies whether the values of the two objects are unequal.
        /// </summary>
        /// <param name="left">The left <see cref="DxgiAdapterDesc"/> to compare.</param>
        /// <param name="right">The right <see cref="DxgiAdapterDesc"/> to compare.</param>
        /// <returns><value>true</value> if the values of left and right differ; otherwise, <value>false</value>.</returns>
        public static bool operator !=(DxgiAdapterDesc left, DxgiAdapterDesc right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.adapterDescription;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><value>true</value> if the specified object is equal to the current object; otherwise, <value>false</value>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is DxgiAdapterDesc))
            {
                return false;
            }

            return this.Equals((DxgiAdapterDesc)obj);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><value>true</value> if the specified object is equal to the current object; otherwise, <value>false</value>.</returns>
        public bool Equals(DxgiAdapterDesc other)
        {
            return this.adapterDescription == other.adapterDescription
                && this.vendorId == other.vendorId
                && this.deviceId == other.deviceId
                && this.subSysId == other.subSysId
                && this.revision == other.revision
                && this.dedicatedVideoMemory == other.dedicatedVideoMemory
                && this.dedicatedSystemMemory == other.dedicatedSystemMemory
                && this.sharedSystemMemory == other.sharedSystemMemory
                && this.adapterLuid == other.adapterLuid;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return new
            {
                this.adapterDescription,
                this.vendorId,
                this.deviceId,
                this.subSysId,
                this.revision,
                this.dedicatedVideoMemory,
                this.dedicatedSystemMemory,
                this.sharedSystemMemory,
                this.adapterLuid
            }
            .GetHashCode();
        }
    }

    [ComImport, Guid("2411e7e1-12ac-4ccf-bd14-9798e8534dc0")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDxgiAdapter
    {
        /// <summary>
        /// Sets application-defined data to the object and associates that data with a GUID.
        /// </summary>
        /// <param name="name">A GUID that identifies the data.</param>
        /// <param name="dataSize">The size of the object's data.</param>
        /// <param name="data">A pointer to the object's data.</param>
        void SetPrivateData(
            [In] ref Guid name,
            [In] uint dataSize,
            [In, MarshalAs(UnmanagedType.LPArray)] byte[] data);

        /// <summary>
        /// Set an interface in the object's private data.
        /// </summary>
        /// <param name="name">A GUID identifying the interface.</param>
        /// <param name="unknown">The interface to set.</param>
        void SetPrivateDataInterface(
            [In] ref Guid name,
            [In, MarshalAs(UnmanagedType.IUnknown)] object unknown);

        /// <summary>
        /// Get a pointer to the object's data.
        /// </summary>
        /// <param name="name">A GUID identifying the data.</param>
        /// <param name="dataSize">The size of the data.</param>
        /// <param name="data">Pointer to the data.</param>
        void GetPrivateData(
            [In] ref Guid name,
            [In, Out] ref uint dataSize,
            [Out, MarshalAs(UnmanagedType.LPArray)] byte[] data);

        /// <summary>
        /// Gets the parent of the object.
        /// </summary>
        /// <param name="riid">The ID of the requested interface.</param>
        /// <returns>The address of a pointer to the parent object.</returns>
        [return: MarshalAs(UnmanagedType.IUnknown)]
        object GetParent(
            [In] ref Guid riid);

        /// <summary>
        /// Enumerate adapter (video card) outputs.
        /// </summary>
        /// <param name="uindex">The index of the output.</param>
        /// <param name="output">The address of a pointer to an <c>IDXGIOutput</c> interface at the position specified by the Output parameter.</param>
        /// <returns>A code that indicates success or failure. Will return <value>DXGI_ERROR_NOT_FOUND</value> if the index is greater than the number of outputs.</returns>
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Bool)]
        bool EnumOutputs(
            [In] uint uindex,
            [Out] out IntPtr output);

        /// <summary>
        /// Gets a DXGI 1.0 description of an adapter (or video card).
        /// </summary>
        /// <returns>A <c>DXGI_ADAPTER_DESC</c> structure that describes the adapter.</returns>
        DxgiAdapterDesc GetDesc();

        /// <summary>
        /// Checks whether the system supports a device interface for a graphics component.
        /// </summary>
        /// <param name="name">The GUID of the interface of the device version for which support is being checked.</param>
        /// <param name="umdVersion">The user mode driver version of interface's name.</param>
        /// <returns><value>S_OK</value> indicates that the interface is supported, otherwise <value>DXGI_ERROR_UNSUPPORTED</value>.</returns>
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Bool)]
        bool CheckInterfaceSupport(
            [In] ref Guid name,
            [Out] out long umdVersion);
    }

}
