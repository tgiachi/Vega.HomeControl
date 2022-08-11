using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vega.HomeControl.Api.Utils
{
    public enum OsTypeEnum
    {
        Win,
        Linux,
        Os64,
        OsArm64
    }

    public class OsUtils
    {
        public static OsTypeEnum GetOs()
        {
            if (RuntimeInformation
                    .ProcessArchitecture == Architecture.Arm64)
            {
                return OsTypeEnum.OsArm64;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return OsTypeEnum.Win;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return OsTypeEnum.Linux;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return OsTypeEnum.Os64;

            return OsTypeEnum.Linux;
        }
    }
}
