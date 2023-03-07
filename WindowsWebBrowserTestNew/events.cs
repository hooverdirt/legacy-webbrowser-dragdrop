using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WindowsWebBrowserTest {
    public delegate void WBDragEnterEventHandler(object sender, WBDragEnterEventArgs e);
    public class WBDragEnterEventArgs : System.EventArgs {
        public WBDragEnterEventArgs() { }
        public void SetParameters(DataObject DropDataObject, uint KeyState, System.Drawing.Point pt, uint Effect) {
            this.keystate = KeyState;
            this.pt = pt;
            this.dropeffect = (DROPEFFECT)Effect;
            this.handled = false;
            this.dataobject = DropDataObject;
        }

        public uint keystate;
        public System.Drawing.Point pt;
        public DROPEFFECT dropeffect;
        public DataObject dataobject;
        public bool handled;
    }

    public delegate void WBDragOverEventHandler(object sender, WBDragOverEventArgs e);
    public class WBDragOverEventArgs : System.EventArgs {
        public WBDragOverEventArgs() { }
        public void SetParameters(uint KeyState, System.Drawing.Point pt, uint Effect) {
            this.keystate = KeyState;
            this.pt = pt;
            this.dropeffect = (DROPEFFECT)Effect;
            this.handled = false;
        }

        public uint keystate;
        public System.Drawing.Point pt;
        public DROPEFFECT dropeffect;
        public bool handled;
    }

    public delegate void WBDragLeaveEventHandler(object sender);

    public delegate void WBDropEventHandler(object sender, WBDropEventArgs e);
    public class WBDropEventArgs : System.EventArgs {
        public WBDropEventArgs() { }
        public void SetParameters(uint KeyState, System.Drawing.Point pt, DataObject DropDataObject, uint Effect) {
            this.handled = false;
            this.keystate = KeyState;
            this.pt = pt;
            this.dropeffect = (DROPEFFECT)Effect;
            this.dataobject = DropDataObject;
        }

        public uint keystate;
        public System.Drawing.Point pt;
        public DROPEFFECT dropeffect;
        public DataObject dataobject;
        public bool handled;
    }

}
