using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class ClipboardMonitor
    {

        private ClipboardFormat lastFormat;
        private object lastData;

        public ClipboardMonitor()
        {

        }

        public string detectChange()
        {
            
            string res = "";
            //IDataObject idat = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {


                        IDataObject iData = Clipboard.GetDataObject();

                        if (iData == null)
                        {
                            return;
                        }

                        ClipboardFormat? format = null;

                        if (Clipboard.ContainsText())
                        {
                            format = ClipboardFormat.Text;
                        }
                        else if (Clipboard.ContainsImage())
                        {
                            format = ClipboardFormat.Bitmap;
                        }

                        object data = iData.GetData(format.ToString());

                        if (data == null || format == null)
                            return;


                        if (format+"" != (ClipboardFormat)lastFormat+"" || data+"" != lastData+"")
                        {
                            //MessageBox.Show("Changed!\n\n" + format + " : " + lastFormat + "\n\n" + data + " : " + lastData);
                            lastFormat = (ClipboardFormat)format;
                            lastData = data;
                            if ((format + "").Contains("Text") == true)
                            {
                                res = "Text";
                            }else if ((format + "").Contains("Image") == true)
                            {
                                res = "Image";
                            }
                            else
                            {
                                res = "None";
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.IsBackground = true;
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return res;
        }



        const int WM_DRAWCLIPBOARD = 0x308;
        const int WM_CHANGECBCHAIN = 0x030D;

        public ClipboardFormat LastFormat { get => lastFormat; set => lastFormat = value; }
        public object LastData { get => lastData; set => lastData = value; }
    }

    public enum ClipboardFormat : byte
    {
        /// <summary>Specifies the standard ANSI text format. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        Text,
        /// <summary>Specifies the standard Windows Unicode text format. This static field
        /// is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        UnicodeText,
        /// <summary>Specifies the Windows device-independent bitmap (DIB) format. This static
        /// field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Dib,
        /// <summary>Specifies a Windows bitmap format. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Bitmap,
        /// <summary>Specifies the Windows enhanced metafile format. This static field is
        /// read-only.</summary>
        /// <filterpriority>1</filterpriority>
        EnhancedMetafile,
        /// <summary>Specifies the Windows metafile format, which Windows Forms does not
        /// directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        MetafilePict,
        /// <summary>Specifies the Windows symbolic link format, which Windows Forms does
        /// not directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        SymbolicLink,
        /// <summary>Specifies the Windows Data Interchange Format (DIF), which Windows Forms
        /// does not directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Dif,
        /// <summary>Specifies the Tagged Image File Format (TIFF), which Windows Forms does
        /// not directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Tiff,
        /// <summary>Specifies the standard Windows original equipment manufacturer (OEM)
        /// text format. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        OemText,
        /// <summary>Specifies the Windows palette format. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        Palette,
        /// <summary>Specifies the Windows pen data format, which consists of pen strokes
        /// for handwriting software, Windows Forms does not use this format. This static
        /// field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        PenData,
        /// <summary>Specifies the Resource Interchange File Format (RIFF) audio format,
        /// which Windows Forms does not directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Riff,
        /// <summary>Specifies the wave audio format, which Windows Forms does not directly
        /// use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        WaveAudio,
        /// <summary>Specifies the Windows file drop format, which Windows Forms does not
        /// directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        FileDrop,
        /// <summary>Specifies the Windows culture format, which Windows Forms does not directly
        /// use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Locale,
        /// <summary>Specifies text consisting of HTML data. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        Html,
        /// <summary>Specifies text consisting of Rich Text Format (RTF) data. This static
        /// field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Rtf,
        /// <summary>Specifies a comma-separated value (CSV) format, which is a common interchange
        /// format used by spreadsheets. This format is not used directly by Windows Forms.
        /// This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        CommaSeparatedValue,
        /// <summary>Specifies the Windows Forms string class format, which Windows Forms
        /// uses to store string objects. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        StringFormat,
        /// <summary>Specifies a format that encapsulates any type of Windows Forms object.
        /// This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Serializable,
    }

}