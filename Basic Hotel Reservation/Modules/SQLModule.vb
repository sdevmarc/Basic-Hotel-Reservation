Imports MySql.Data.MySqlClient
Module SQLModule
    Dim connstring As String = "server=localhost; user=root; password=1234; database=hotel_reservation;"
    Dim con As New MySqlConnection(connstring)
    Dim cmd As MySqlCommand

    Sub ConnectDB(inp As Object)
        If (inp = "con") Then
            con.Open()
        ElseIf (inp = "dis") Then
            con.Close()
        End If
    End Sub

    'SHOW
    Sub ShowData(grid As Object)
        cmd = New MySqlCommand("Select * from customer left join room on customer.customer_id = room.room_id", con)
        Dim read As MySqlDataReader = cmd.ExecuteReader
        While read.Read
            grid.Rows.Add(read("customer_id"), read("lastname"), read("firstname"), read("email"), read("contact_no"), read("room_no"), read("roomtype"), read("check_in"), read("check_out"))
        End While
        read.Close()
    End Sub

    'ADD
    Sub AddData()
        Dim AddToCus As String = "insert into customer (lastname, firstname, contact_no, email, check_in, check_out) values (@LN, @FN, @CN, @EM, @CI, @CO)"
        cmd = New MySqlCommand(AddToCus, con)
        cmd.Parameters.AddWithValue("@LN", AddReservation.txtLN.Text)
        cmd.Parameters.AddWithValue("@FN", AddReservation.txtFN.Text)
        cmd.Parameters.AddWithValue("@CN", AddReservation.txtCN.Text)
        cmd.Parameters.AddWithValue("@EM", AddReservation.txtEmail.Text)
        cmd.Parameters.AddWithValue("@CI", AddReservation.dtpCI.Text)
        cmd.Parameters.AddWithValue("@CO", AddReservation.dtpCO.Text)
        cmd.ExecuteNonQuery()

        Dim AddToRoo As String = "insert into room (room_no, roomtype) values (@RN, @RT)"
        cmd = New MySqlCommand(AddToRoo, con)
        cmd.Parameters.AddWithValue("@RN", OBJModule.value(AddReservation.cmbRT))
        cmd.Parameters.AddWithValue("@RT", AddReservation.cmbRT.SelectedItem)

        cmd.ExecuteNonQuery()
        MessageBox.Show("The booked has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'SEARCH
    Sub SearchData(ID As Object, grid As Object)
        grid.Rows.Clear()
        cmd = New MySqlCommand("Select * from customer left join room on customer.customer_id = room.room_id where customer_id like '%" & ID & "%'", con)
        Dim read As MySqlDataReader = cmd.ExecuteReader
        While read.Read
            grid.Rows.Add(read("customer_id"), read("lastname"), read("firstname"), read("email"), read("contact_no"), read("room_no"), read("roomtype"), read("check_in"), read("check_out"))
        End While
        read.Close()
    End Sub

    'EDIT BUTTON
    'TEXTBOX DB RETRIEVAL
    Sub RetrieveTxt(ID As Object)
        cmd = New MySqlCommand("Select * from customer left join room on customer.customer_id = room.room_id where customer_id like '%" & ID & "%'", con)
        Dim read As MySqlDataReader = cmd.ExecuteReader
        While read.Read
            MngReservation.txtLN.Text = read.GetString("lastname")
            MngReservation.txtFN.Text = read.GetString("firstname")
            MngReservation.txtCN.Text = read.GetString("contact_no")
            MngReservation.txtEmail.Text = read.GetString("email")
            MngReservation.cmbRT.SelectedItem = read.GetString("roomtype")
            MngReservation.dtpCI.Value = read.GetString("check_in")
            MngReservation.dtpCO.Value = read.GetString("check_out")
        End While
        read.Close()
    End Sub

    'UPDATE
    Sub UpdateData(inp As Object)
        cmd = New MySqlCommand("Update customer set lastname = @ln, firstname = @fn, contact_no = @cn, email = @em, check_in = @ci, check_out = @co where customer_id = @id", con)
        cmd.Parameters.AddWithValue("@ln", MngReservation.txtLN.Text)
        cmd.Parameters.AddWithValue("@fn", MngReservation.txtFN.Text)
        cmd.Parameters.AddWithValue("@cn", MngReservation.txtCN.Text)
        cmd.Parameters.AddWithValue("@em", MngReservation.txtEmail.Text)
        cmd.Parameters.AddWithValue("@ci", MngReservation.dtpCI.Text)
        cmd.Parameters.AddWithValue("@co", MngReservation.dtpCO.Text)
        cmd.Parameters.AddWithValue("@id", Main.txtSearch.Text)
        cmd.ExecuteNonQuery()

        cmd = New MySqlCommand("update room set room_no = @rn, roomtype = @rt where room_id = @id", con)
        cmd.Parameters.AddWithValue("@rn", OBJModule.value(MngReservation.cmbRT))
        cmd.Parameters.AddWithValue("@rt", MngReservation.cmbRT.SelectedItem)
        cmd.Parameters.AddWithValue("@id", Main.txtSearch.Text)
        cmd.ExecuteNonQuery()
        MessageBox.Show("Data has been Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        inp.close()
        Main.Show()
    End Sub

    'DELETE
    Sub DeleteData()
        cmd = New MySqlCommand("delete from customer  where customer_id = @id", con)
        cmd.Parameters.AddWithValue("id", Main.txtSearch.Text)
        cmd.ExecuteNonQuery()

        cmd = New MySqlCommand("delete from room where room_id = @id", con)
        cmd.Parameters.AddWithValue("@id", Main.txtSearch.Text)
        cmd.ExecuteNonQuery()
    End Sub
End Module
