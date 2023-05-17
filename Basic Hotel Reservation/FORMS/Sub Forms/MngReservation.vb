Public Class MngReservation
    Private Sub MngReservation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Arr(cmbRT)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        UpdateData(Me)
    End Sub
End Class