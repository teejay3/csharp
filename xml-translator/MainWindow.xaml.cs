/*
 * 
 * 
 * 
 * 
 * 
 * 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Schema;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Text;

namespace xml_app
{
    public partial class MainWindow : Window
    {
        string xml_path;        //путь к исходному xml
        string xsd_path;        //путь к файлу стиля исходного xml
        string xslt_path;       //файл трансформации
        string out_xsd_path;    //файл стиля транформированного xml
        string out_xml;         //конечный xml

        public MainWindow()
        {
            InitializeComponent();
            tbox.Clear();
        }

        public bool Validate(string xml, string xsd)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            try { settings.Schemas.Add(null, xsd); }
            catch (Exception ex) { tbox.AppendText("XSD file error: " + ex.Message + '\n');  return false; }
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

            settings.ValidationEventHandler += (o, args) =>
            {
                //MessageBox.Show("ValidationEventHandler");
                if (args.Severity == XmlSeverityType.Error)
                    tbox.AppendText("\nError: " +
                        args.Message + " Line number: " +
                        args.Exception.LineNumber + " Position number: " + args.Exception.LinePosition + '\n');
                throw new System.Exception();
            };

            Uri res;
            XmlReader reader;
            if (Uri.TryCreate(xml, UriKind.Absolute, out res))
            {reader = XmlReader.Create(xml, settings);}
            else
            {reader = XmlReader.Create(new StringReader(xml), settings);}
            try
            {
                while (reader.Read());
            }
            catch (XmlException xmEx)
            {
                tbox.AppendText("\nError: " + xmEx.Message);
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                reader.Dispose();
                reader.Close();
            }
            return true;
        }

        private void Select_input_xml(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xlm";
            dlg.Filter = "xml files (*.xml)|*.xml|All files(*.*)|*.*";
            dlg.RestoreDirectory = true;
            Nullable<bool> result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                xml_path = dlg.FileName;
                i_xml.Content = dlg.SafeFileName;
            }
        }
        private void Select_input_xsd(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xsd";
            dlg.Filter = "xsd files (*.xsd)|*.xsd|All files(*.*)|*.*";
            dlg.RestoreDirectory = true;
            Nullable<bool> result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                xsd_path = dlg.FileName;
                i_xsd.Content = dlg.SafeFileName;
            }
        }
        private void Select_input_xslt(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xsl";
            dlg.Filter = "xslt files (*.xsl)|*.xsl|All files(*.*)|*.*";
            dlg.RestoreDirectory = true;
            Nullable<bool> result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                xslt_path = dlg.FileName;
                i_xslt.Content = dlg.SafeFileName;
            }
        }
        private void Select_output_xsd(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xsd";
            dlg.Filter = "xsd files (*.xsd)|*.xsd|All files(*.*)|*.*";
            dlg.RestoreDirectory = true;
            Nullable<bool> result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                out_xsd_path = dlg.FileName;
                o_xml.Content = dlg.SafeFileName;
            }
        }

        private void Transform_click(object sender, RoutedEventArgs e)
        {
            tbox.Clear();
            if (xml_path == null || xml_path.Length == 0)
            {
                tbox.AppendText("Check the name for input XML file");
                return;
            }
            else if (xsd_path == null || xsd_path.Length == 0)
            {
                tbox.AppendText("Check the name for input XSD file");
                return;
            }
            else if (xslt_path == null || xslt_path.Length == 0)
            {
                tbox.AppendText("\nCheck the name for XSLT file!\n");
                return;
            }
            else if (out_xsd_path == null || out_xsd_path.Length == 0)
            {
                tbox.AppendText("\nCheck the name for output XSD file!\n");
                return;
            }

#region Initial validation
            tbox.AppendText("Starting initial validation...\n");
            if (Validate(xml_path, xsd_path))
                tbox.AppendText("Initial validation successfull.\n");
            else
            {
                tbox.AppendText("\nValidation failed\n");
                return;
            }
#endregion

#region TRANSFORM
            
            XDocument newTree = new XDocument();
            using (XmlWriter writer = XmlWriter.Create(newTree.CreateWriter()))
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                tbox.AppendText("\nStarting transformation...\n");
                try
                {
                     xslt.Load(xslt_path);
                     xslt.Transform(xml_path, writer);
                }
                catch (Exception ex)
                { tbox.AppendText("Transformation error: " + ex.Message + " Check XSLT file!\n"); return;}
                tbox.AppendText("Transformation complete\n");
            }
            
#endregion

#region Final validation
            tbox.AppendText("\nStarting final validation...\n");
            if (Validate(newTree.ToString(), out_xsd_path))
                tbox.AppendText("Final validation successfull\n");
            else
            {
                tbox.AppendText("Validation error\n");
                return;
            }
#endregion

#region File save
            tbox.AppendText("\nSaving output XML file...\n");
            if (output_textBox.Text.Length > 0) out_xml = output_textBox.Text;
            else { out_xml = "out.xml";}
            try
            {
                File.WriteAllText(new Uri(Directory.GetCurrentDirectory() + "\\" + out_xml, UriKind.Absolute).LocalPath, newTree.ToString());
                tbox.AppendText("File saved: " + (new Uri(Directory.GetCurrentDirectory())).ToString() +"/"+ out_xml);
            }
            catch (Exception ex)
            {
                tbox.AppendText ("Can't save file!\nError: " + ex.Message + "\nUnable to save file " + out_xml);
                return;
            }
#endregion
        }
    }
}