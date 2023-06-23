<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/657617460/17.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1174024)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# WinForms DiagramControl - Register FactoryItemTools for Regular and Custom Shapes

This example demonstrates how to register `FactoryItemTools` for regular shapes and shapes that are created from SVG images or ShapeTemplates. `FactoryItemTools` allow you to create custom tools that are available in the Toolbox at runtime.
To register a `FactoryItemTool` you need to [create](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.DiagramStencil.Create.overloads) a stencil or use one of existed stencils and pass your `FactoryItemTool` instance to the `RegisterTool` method:

```cs
public void RegularFactoryItemTool(DiagramStencil stencil) {
	var itemTool = new FactoryItemTool("CustomShape1",
	   () => "Custom Shape 1",
	   diagram => new DiagramShape() { Content = "Predefined text" },
	   new System.Windows.Size(200, 200), false);

	stencil.RegisterTool(itemTool);
}
```

If you use a custom shape and wish to register a custom tool for it, it's necessary to use two stencils:
1) An [invisible stencil](https://docs.devexpress.com/WindowsForms/116881/controls-and-libraries/diagrams/diagram-control/shapes-panel#create-hidden-stencils) that contains your custom shape;
2) A visible instance that contains the custom tool. This tool should access the custom shape by its `ID` from the invisible stencil:

```cs
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
```

## Files to Review

- link.cs (VB: link.vb)
- link.js
- ...

## Documentation

- link
- link
- ...

## More Examples

- link
- link
- ...
