using System;

namespace WindowsWebBrowserTest {
    public class SecurityManager : IInternetSecurityManager {
        #region IInternetSecurityManager Members

        public int SetSecuritySite(IntPtr pSite) {
            throw new NotImplementedException();
        }

        public int GetSecuritySite(IntPtr pSite) {
            throw new NotImplementedException();
        }

        public int MapUrlToZone(string pwszUrl, out uint pdwZone, uint dwFlags) {
            throw new NotImplementedException();
        }

        public int GetSecurityId(string pwszUrl, byte[] pbSecurityId, ref uint pcbSecurityId, uint dwReserved) {
            throw new NotImplementedException();
        }

        public int ProcessUrlAction(string pwszUrl, uint dwAction, out byte pPolicy, uint cbPolicy, byte pContext, uint cbContext, uint dwFlags, uint dwReserved) {
            throw new NotImplementedException();
        }

        public int QueryCustomPolicy(string pwszUrl, ref Guid guidKey, ref byte ppPolicy, ref uint pcbPolicy, ref byte pContext, uint cbContext, uint dwReserved) {
            throw new NotImplementedException();
        }

        public int SetZoneMapping(uint dwZone, string lpszPattern, uint dwFlags) {
            throw new NotImplementedException();
        }

        public int GetZoneMappings(uint dwZone, out System.Runtime.InteropServices.ComTypes.IEnumString ppenumString, uint dwFlags) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
