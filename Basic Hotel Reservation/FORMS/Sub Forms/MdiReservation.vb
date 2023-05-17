Public Class MdiReservation
    Private Sub MdiReservation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowData(dgvReservations)
        SearchData(Main.txtSearch.Text, dgvReservations)
    End Sub

    Private Sub ResCellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReservations.CellClick
        Highlight(dgvReservations, e)
    End Sub
End Class