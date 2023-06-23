using DevExpress.Diagram.Core;
using DevExpress.Utils.Serializing;
using DevExpress.XtraDiagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            RegisterStencil();
        }

        void RegisterStencil() {
            var stencil = new DiagramStencil("CustomStencil", "Custom Shapes");
            RegularFactoryItemTool(stencil);
            FactoryItemToolForCustomShape(stencil);

            DiagramToolboxRegistrator.RegisterStencil(stencil);

            diagramControl1.OptionsBehavior.SelectedStencils = new StencilCollection() { "CustomStencil" };
        }

        public void RegularFactoryItemTool(DiagramStencil stencil) {
            var itemTool = new FactoryItemTool("CustomShape1",
               () => "Custom Shape 1",
               diagram => new DiagramShape() { Content = "Predefined text" },
               new System.Windows.Size(200, 200), false);

            stencil.RegisterTool(itemTool);
        }


        public void FactoryItemToolForCustomShape(DiagramStencil stencil) {
            DiagramControl.ItemTypeRegistrator.Register(typeof(DiagramShapeEx));
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WindowsFormsApp4.CustomShapes.xml");
            var invisibleStencil = DiagramStencil.Create("InvisibleStencil", "Invisible Stencil", stream, shapeName => shapeName, false);
            DiagramToolboxRegistrator.RegisterStencil(invisibleStencil);

            var itemTool = new FactoryItemTool("CustomShape2",
               () => "Custom Shape 2",
               diagram => new DiagramShapeEx() { Shape = DiagramToolboxRegistrator.GetStencil("InvisibleStencil").GetShape("Shape1"), CustomProperty = "Some value" },
               new System.Windows.Size(200, 200), false);

            stencil.RegisterTool(itemTool);
        }
    }

    public class DiagramShapeEx : DiagramShape
    {
        [XtraSerializableProperty]
        public string CustomProperty { get; set; }
    }
}
