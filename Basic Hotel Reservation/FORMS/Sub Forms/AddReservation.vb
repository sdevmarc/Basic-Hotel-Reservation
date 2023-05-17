Public Class AddReservation
    Private Sub AddReservation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Arr(cmbRT)
    End Sub
    Private Sub btnAddBook_Click(sender As Object, e As EventArgs) Handles btnAddBook.Click
        AddData()
        Main.Show()
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Main.Show()
        Me.Close()
    End Sub
End Class