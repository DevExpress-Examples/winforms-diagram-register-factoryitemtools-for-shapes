<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/657617460/17.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1174024)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WinForms DiagramControl - Register FactoryItemTools for Regular and Custom Shapes

This example demonstrates how to register `FactoryItemTools` for regular shapes and shapes created from SVG images or `ShapeTemplates`. The [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool) allows you to add pre-configured or customized shapes and their descendants to stencils. The [DiagramControl](https://docs.devexpress.com/WindowsForms/DevExpress.XtraDiagram.DiagramControl) displays shapes specified by registered `FactoryItemTools` in the [Shapes Panel](https://docs.devexpress.com/WindowsForms/116881/controls-and-libraries/diagrams/diagram-control/shapes-panel).

## Implementation Details

Follow the steps below to register a [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool) for a regular shape:

1. [Create](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.DiagramStencil.Create.overloads) a stencil or use one of the existing stencils:

   ```cs
   void RegisterStencil() {
       var stencil = new DiagramStencil("CustomStencil", "Custom Shapes");
       RegularFactoryItemTool(stencil);
       FactoryItemToolForCustomShape(stencil);
       DiagramToolboxRegistrator.RegisterStencil(stencil);
       diagramControl1.OptionsBehavior.SelectedStencils = new StencilCollection() { "CustomStencil" };
   }
   ```

2. Create a [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool) that specifies a diagram shape.
3. Pass your [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool) instance to the [DiagramStencil.RegisterTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.DiagramStencil.RegisterTool(DevExpress.Diagram.Core.ItemTool)) method:

   ```cs
   public void RegularFactoryItemTool(DiagramStencil stencil) {
       var itemTool = new FactoryItemTool("CustomShape1",
           () => "Custom Shape 1",
           diagram => new DiagramShape() { Content = "Predefined text" },
           new System.Windows.Size(200, 200), 
           false
       );

       stencil.RegisterTool(itemTool);
   }
   ```

You should create two stencils to register a [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool) for a custom shape:

1. Create an [invisible stencil](https://docs.devexpress.com/WindowsForms/116881/controls-and-libraries/diagrams/diagram-control/shapes-panel#create-hidden-stencils) that contains your custom shape.
2. Create a visible stencil that contains the custom tool. This tool should access your custom shape by its `ID` from the invisible stencil:

   ```cs
   public void FactoryItemToolForCustomShape(DiagramStencil stencil) {
       DiagramControl.ItemTypeRegistrator.Register(typeof(DiagramShapeEx));
       var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WindowsFormsApp4.CustomShapes.xml");
       var invisibleStencil = DiagramStencil.Create("InvisibleStencil", "Invisible Stencil", stream, shapeName => shapeName, false);
       DiagramToolboxRegistrator.RegisterStencil(invisibleStencil);

       var itemTool = new FactoryItemTool("CustomShape2",
           () => "Custom Shape 2",
           diagram => new DiagramShapeEx() { 
               Shape = DiagramToolboxRegistrator.GetStencil("InvisibleStencil").GetShape("Shape1"), 
               CustomProperty = "Some value" 
           },
           new System.Windows.Size(200, 200), 
           false
       );

       stencil.RegisterTool(itemTool);
   }
   ```

## Files to Review

- [Form1.cs](./CS/WindowsFormsApp4/Form1.cs) (VB: [Form1.vb](./VB/WindowsFormsApp4/Form1.vb))
- [CustomShapes.xml](./CS/WindowsFormsApp4/CustomShapes.xml)

## Documentation

- [Use Shape Templates to Create Shapes and Containers](https://docs.devexpress.com/WindowsForms/17764/controls-and-libraries/diagrams/diagram-items/creating-shapes-and-containers-using-shape-templates)
- [SVG Shapes](https://docs.devexpress.com/WindowsForms/117671/controls-and-libraries/diagrams/diagram-items/svg-shapes)
- [FactoryItemTool](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.FactoryItemTool)

## More Examples

- [WinForms DiagramControl - Create Custom Shapes Based on Diagram Containers](https://github.com/DevExpress-Examples/winforms-diagram-create-custom-shapes-based-on-diagram-containers)
- [Diagram Control for WinForms - Create Custom Shapes with Connection Points](https://github.com/DevExpress-Examples/winforms-diagram-create-custom-shapes-with-connection-points)
