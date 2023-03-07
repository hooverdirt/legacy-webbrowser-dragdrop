using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace WindowsWebBrowserTest {
    public partial class InternalWebBrowser : UserControl, IOleClientSite, IDocHostUIHandler, IDropTarget, IServiceProviderAuthenticate {
        private WebBrowser webbrowser = null;
        private bool useInternalDragDrop = true;
        private object externalObject = null;
        public event WBDragEnterEventHandler OnWBDragEnter = null;
        public event WBDragLeaveEventHandler OnWBDragLeave = null;
        public event WBDragOverEventHandler OnWBDragOver = null;
        public event WBDropEventHandler OnWBDragDrop = null;


        private WBDragEnterEventArgs WBDragEnterEvent = new WBDragEnterEventArgs();
        private WBDragOverEventArgs WBDragOverEvent = new WBDragOverEventArgs();
        private WBDropEventArgs WBDropEvent = new WBDropEventArgs();
        private IWebBrowser2 wbWebBrowser2 = null;


        // OLE Stuff...
        private object wbUnknown = null;
        private IOleObject wbOleObject = null;
        private IOleInPlaceObject wbOleInPlaceObject = null;
        private IOleCommandTarget wbOleCommandTarget = null;



        public InternalWebBrowser() {
            InitializeComponent();
            initWebbrowser();
        }

        private void initWebbrowser() {
            webbrowser = new WebBrowser();
            webbrowser.Parent = this;
            this.webbrowser.Dock = DockStyle.Fill;
            webbrowser.AllowWebBrowserDrop = false;
           
            this.useInternalDragDrop = true;

            wbOleObject = (webbrowser.ActiveXInstance as IOleObject);


            // this needs to be reset to null after destroying the component...
            int iret = wbOleObject.SetClientSite(this);

            //Set hostnames
            iret = wbOleObject.SetHostNames("InternalWebBrowser", string.Empty);

            // not in use... testing purpose only
            wbOleInPlaceObject = (IOleInPlaceObject)wbUnknown;

            // not in use...
            wbWebBrowser2 = (IWebBrowser2)wbUnknown;

        }

        public bool UseDragAndDrop {
            get { return this.useInternalDragDrop; }
            set { this.useInternalDragDrop = value; }
        }

        public WebBrowser WebBrowser {
            get { return this.webbrowser; }
            set { this.webbrowser = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public object ExternalObject {
            get { return this.externalObject; }
            set { this.externalObject = value; }
        }


        public IHTMLElement ElementFromPoint(int clientx, int clienty) {
            IHTMLElement elem = null;
            int offsetx = 0;
            int offsety = 0;
            string tagname = string.Empty;

            //Get the main document
            // IHTMLDocument2 pdoc2 = m_WBWebBrowser2.Document as IHTMLDocument2;
            // not sure if this is safe... but since this is our container....
            Point px = this.PointToClient(new Point(clientx, clienty));

            clientx = px.X;
            clienty = px.Y;

            // we should probably verify if this is still going good as I'm taking the ActiveXInstance..
            IHTMLDocument2 pdoc2 = (this.webbrowser.ActiveXInstance as IWebBrowser2).Document as IHTMLDocument2;

            if (pdoc2 != null)
                elem = pdoc2.elementFromPoint(clientx, clienty);

            if ((elem != null) && (!string.IsNullOrEmpty(elem.tagName)))
                tagname = elem.tagName.ToLower();

            if ((!string.IsNullOrEmpty(tagname)) &&
                ((tagname == "frame") || (tagname == "iframe"))
                ) {
                IHTMLElement offsetparent = elem.offsetParent;
                IHTMLElement2 elem2 = null;

                //Account for offsetLeft, offsetTop, scrollLeft, scrollTop

                //First get the main document scrolltop and scrollleft
                IHTMLDocument3 pdoc3 = wbWebBrowser2.Document as IHTMLDocument3;
                if (pdoc3 != null) {
                    elem2 = pdoc3.documentElement as IHTMLElement2;
                    if (elem2 != null) {
                        offsetx -= elem2.scrollLeft;
                        offsety -= elem2.scrollTop;
                    }
                }

                //This is needed if we have a fame
                elem2 = elem as IHTMLElement2;
                if (elem2 != null) {
                    offsetx -= elem2.scrollLeft;
                    offsety -= elem2.scrollTop;
                }
                offsetx += elem.offsetLeft;
                offsety += elem.offsetTop;

                //For frame+iframe
                while (offsetparent != null) {
                    offsetx += offsetparent.offsetLeft;
                    offsety += offsetparent.offsetTop;
                    offsetparent = offsetparent.offsetParent;
                }

                //Cast it to IWebBrowser2
                IWebBrowser2 pwb = elem as IWebBrowser2;
                if (pwb != null) {
                    //Now get the document
                    //If you attempt to access the document via IHTMLFrameBase2.contentWindow
                    //you will get a "Access Denied" exception
                    pdoc2 = pwb.Document as IHTMLDocument2;

                    IHTMLElement loopelem = null;
                    if (pdoc2 != null) {
                        elem = pdoc2.elementFromPoint(clientx - offsetx, clienty - offsety);
                        loopelem = elem;
                        //There is only one website that I know of that has so many nested frames
                        //MSDN archive website.
                        while ((loopelem != null) &&
                            (loopelem.tagName.ToLower() == "frame" ||
                            loopelem.tagName.ToLower() == "iframe")) {
                            pwb = loopelem as IWebBrowser2;
                            if (pwb == null)
                                break;
                            pdoc2 = pwb.Document as IHTMLDocument2;
                            if (pdoc2 == null)
                                break;

                            if (elem.offsetParent != null) {
                                offsetx += elem.offsetParent.offsetLeft;
                                offsety += elem.offsetParent.offsetTop;
                            }

                            offsetx += elem.offsetLeft;
                            offsety += elem.offsetTop;

                            loopelem = pdoc2.elementFromPoint(clientx - offsetx, clienty - offsety);
                            if (loopelem != null) {
                                elem = loopelem;
                            }
                        }
                    }
                }
            }

            return elem;
        }

        #region IOleClientSite Members

        public int SaveObject() {
            return Hresults.E_NOTIMPL;
        }

        public int GetMoniker(uint dwAssign, uint dwWhichMoniker, out System.Runtime.InteropServices.ComTypes.IMoniker ppmk) {
            ppmk = null;
            return Hresults.E_NOTIMPL;
        }

        public int GetContainer(out IOleContainer ppContainer) {
            ppContainer = null;
            return Hresults.E_NOTIMPL;
        }

        public int ShowObject() {
            return Hresults.S_OK;
        }

        public int OnShowWindow(bool fShow) {
            return Hresults.E_NOTIMPL;
        }

        public int RequestNewObjectLayout() {
            return Hresults.E_NOTIMPL;            
        }
        #endregion


        #region IDocHostUIHandler Members

        public int ShowContextMenu(uint dwID, ref tagPOINT pt, object pcmdtReserved, object pdispReserved) {
            return Hresults.S_FALSE;
        }

        public int GetHostInfo(ref DOCHOSTUIINFO info) {
            return Hresults.S_OK;
        }

        public int ShowUI(int dwID, IOleInPlaceActiveObject activeObject, IOleCommandTarget commandTarget, IOleInPlaceFrame frame, IOleInPlaceUIWindow doc) {
            return Hresults.S_OK;
        }

        public int HideUI() {
            return Hresults.S_OK;
        }

        public int UpdateUI() {
            return Hresults.S_OK;
        }

        public int EnableModeless(bool fEnable) {
            return Hresults.E_NOTIMPL;
        }

        public int OnDocWindowActivate(bool fActivate) {
            return Hresults.E_NOTIMPL;
        }

        public int OnFrameWindowActivate(bool fActivate) {
            return Hresults.E_NOTIMPL;
        }

        public int ResizeBorder(ref tagRECT rect, IOleInPlaceUIWindow doc, bool fFrameWindow) {
            return Hresults.E_NOTIMPL;
        }

        public int TranslateAccelerator(ref tagMSG msg, ref Guid group, uint nCmdID) {
            return Hresults.E_FAIL;
        }

        public int GetOptionKeyPath(out string pbstrKey, uint dw) {
            pbstrKey = null;
            return Hresults.E_FAIL;
        }

        public int GetDropTarget(IDropTarget pDropTarget, out IDropTarget ppDropTarget) {
            int hret = Hresults.S_FALSE;
            ppDropTarget = this as IDropTarget;
            if (ppDropTarget != null)
                hret = Hresults.S_OK;
            
            return hret;         
        }

        public int GetExternal(out object ppDispatch) {

            if (externalObject == null) {
                externalObject = new Object();
            }

            ppDispatch = externalObject;

            return Hresults.S_OK;
        }

        public int TranslateUrl(uint dwTranslate, string strURLIn, out string pstrURLOut) {
            pstrURLOut = null;
            return Hresults.E_NOTIMPL;
        }

        public int FilterDataObject(System.Runtime.InteropServices.ComTypes.IDataObject pDO, out System.Runtime.InteropServices.ComTypes.IDataObject ppDORet) {
            ppDORet = null;
            return Hresults.E_NOTIMPL;
        }

        #endregion

        #region IDropTarget Members


        // taken over from CsBrowser
        public new int DragEnter(System.Runtime.InteropServices.ComTypes.IDataObject pDataObj, uint grfKeyState, tagPOINT pt, ref uint pdwEffect) {
            if (OnWBDragEnter != null) {
                DataObject obja = null;
                if (pDataObj != null)
                    obja = new DataObject(pDataObj);
                Point ppt = new Point(pt.X, pt.Y);
                WBDragEnterEvent.SetParameters(obja, grfKeyState, ppt, pdwEffect);
                OnWBDragEnter(this, WBDragEnterEvent);
                if (WBDragEnterEvent.handled == true)
                    pdwEffect = (uint)WBDragEnterEvent.dropeffect;
            }
            return Hresults.S_OK;
        }

        public new int DragOver(uint grfKeyState, tagPOINT pt, ref uint pdwEffect) {
            if (OnWBDragOver != null) {
                Point ppt = new Point(pt.X, pt.Y);
                WBDragOverEvent.SetParameters(grfKeyState, ppt, pdwEffect);
                OnWBDragOver(this, WBDragOverEvent);
                if (WBDragOverEvent.handled == true)
                    pdwEffect = (uint)WBDragOverEvent.dropeffect;
            }
            return Hresults.S_OK;
        }

        public new int DragLeave() {
            if (OnWBDragLeave != null)
                OnWBDragLeave(this);
            return Hresults.S_OK;

        }

        public int Drop(System.Runtime.InteropServices.ComTypes.IDataObject pDataObj, uint grfKeyState, tagPOINT pt, ref uint pdwEffect) {
            if (pDataObj == null)
                return Hresults.S_OK;
            if (OnWBDragDrop != null) {
                DataObject obja = new DataObject(pDataObj);
                Point ppt = new Point(pt.X, pt.Y);
                WBDropEvent.SetParameters(grfKeyState, ppt, obja, pdwEffect);
                OnWBDragDrop(this, WBDropEvent);
                if (WBDropEvent.handled == true)
                    pdwEffect = (uint)WBDropEvent.pt.X;
            }
            return Hresults.S_OK;
        }

        #endregion

        #region IServiceProviderAuthenticate Members

        public int QueryService(ref Guid guidService, ref Guid riid, out IntPtr ppvObject) {
            int i = 0;
            IntPtr pp = IntPtr.Zero;
            ppvObject = pp;
            IInternetSecurityManager _ism;   // IInternetSecurityManager interface of ecurityManager COM object
            object securityManager;

            int compareResult = guidService.CompareTo(Iid_Clsids.IID_IInternetSecurityManager);

            if (compareResult == 0) {
                Type t = Type.GetTypeFromCLSID(Iid_Clsids.CLSID_InternetSecurityManager);
                securityManager = Activator.CreateInstance(t);
                _ism = (IInternetSecurityManager)securityManager;

                

                if (t != null) {

                }
            }

            return Hresults.E_NOTIMPL;
        }

        #endregion
    }
}
