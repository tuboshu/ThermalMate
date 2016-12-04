﻿using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ThermalMate.Class
{
    static class Common
    {
        public static void SetClipboard(string context)
        {
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Text, context);
        }

        public static void ClearGroupTextControl(Control groupbox)
        {
            foreach (var control in groupbox.Controls.OfType<TextBox>())
            {
                control.Clear();
            }
        }

        public static void StreamToFile(Stream stream, string filePath, bool isOverlay)
        {
            // 将流读取到字节数组
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            if (File.Exists(filePath) && !isOverlay)
            {
                return;
            }
            var fs = new FileStream(filePath, FileMode.CreateNew);
            var bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();

        }

        public static void ReleaseResource(string resourceName, string filePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            // 辅助方法：获取资源在程序集内部的名称
            //foreach (var file in assembly.GetManifestResourceNames())
            //  MessageBox.Show(file);
            var stream = assembly.GetManifestResourceStream(resourceName);
            StreamToFile(stream, filePath, false);

        }
    }
}
