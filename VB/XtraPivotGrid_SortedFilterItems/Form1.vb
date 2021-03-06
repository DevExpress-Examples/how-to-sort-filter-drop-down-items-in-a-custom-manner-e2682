﻿Imports System
Imports System.Collections
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraPivotGrid.Data

Namespace XtraPivotGrid_SortedFilterItems
	Partial Public Class Form1
		Inherits DevExpress.XtraEditors.XtraForm

		Public Sub New()
			InitializeComponent()
			excelDataSource1.FileName = "SalesPerson.xlsx"
			excelDataSource1.Fill()
			AddHandler pivotGridControl1.CustomFilterPopupItems, AddressOf pivotGridControl1_CustomFilterPopupItems
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			pivotGridControl1.DataSource = excelDataSource1
			pivotGridControl1.BestFit()
		End Sub
		Private Sub pivotGridControl1_CustomFilterPopupItems(ByVal sender As Object, ByVal e As PivotCustomFilterPopupItemsEventArgs)
			If rbCaptionLength.Checked Then
				ArrayList.Adapter(DirectCast(e.Items, IList)).Sort(New FilterItemsComparer())
			End If
		End Sub
		Private Sub rbAlphabeticalSort_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbAlphabeticalSort.CheckedChanged
			fieldProductName1.ShowFilterPopup()
		End Sub
	End Class
	Public Class FilterItemsComparer
		Implements IComparer

		Private Function IComparer_Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
			If Not (TypeOf x Is PivotGridFilterItem) OrElse Not (TypeOf y Is PivotGridFilterItem) Then
				Return 0
			End If
			Dim item1 As PivotGridFilterItem = DirectCast(x, PivotGridFilterItem)
			Dim item2 As PivotGridFilterItem = DirectCast(y, PivotGridFilterItem)
			If item1.ToString().Length = item2.ToString().Length Then
				Return 0
			End If
			If item1.ToString().Length > item2.ToString().Length Then
				Return 1
			End If
			Return -1
		End Function
	End Class
End Namespace