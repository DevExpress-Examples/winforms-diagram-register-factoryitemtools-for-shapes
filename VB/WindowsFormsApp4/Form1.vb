Imports DevExpress.Diagram.Core
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraDiagram
Imports System.Reflection
Imports System.Windows.Forms

Namespace WindowsFormsApp4

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            RegisterStencil()
        End Sub

        Private Sub RegisterStencil()
            Dim stencil = New DiagramStencil("CustomStencil", "Custom Shapes")
            RegularFactoryItemTool(stencil)
            FactoryItemToolForCustomShape(stencil)
            DiagramToolboxRegistrator.RegisterStencil(stencil)
            diagramControl1.OptionsBehavior.SelectedStencils = New StencilCollection() From {"CustomStencil"}
        End Sub

        Public Sub RegularFactoryItemTool(ByVal stencil As DiagramStencil)
            Dim itemTool = New FactoryItemTool("CustomShape1", Function() "Custom Shape 1", Function(diagram) New DiagramShape() With {.Content = "Predefined text"}, New System.Windows.Size(200, 200), False)
            stencil.RegisterTool(itemTool)
        End Sub

        Public Sub FactoryItemToolForCustomShape(ByVal stencil As DiagramStencil)
            DiagramControl.ItemTypeRegistrator.Register(GetType(DiagramShapeEx))
            Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WindowsFormsApp4.CustomShapes.xml")
            Dim invisibleStencil = DiagramStencil.Create("InvisibleStencil", "Invisible Stencil", stream, Function(shapeName) shapeName, False)
            DiagramToolboxRegistrator.RegisterStencil(invisibleStencil)
            Dim itemTool = New FactoryItemTool("CustomShape2", Function() "Custom Shape 2", Function(diagram) New DiagramShapeEx() With {.Shape = DiagramToolboxRegistrator.GetStencil("InvisibleStencil").GetShape("Shape1"), .CustomProperty = "Some value"}, New System.Windows.Size(200, 200), False)
            stencil.RegisterTool(itemTool)
        End Sub
    End Class

    Public Class DiagramShapeEx
        Inherits DiagramShape

        <XtraSerializableProperty>
        Public Property CustomProperty As String
    End Class
End Namespace
