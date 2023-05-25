' Gọi tại event Handles TextBoxxx.KeyPress
'e.Handled = False    '  Cho phép gõ
'e.Handled = True    '  KHÔNG Cho phép gõ
Public Class AllowTextboxValue
    Public Sub AllowIntegerNumber(sender As Object, e As KeyPressEventArgs)     ' Gọi tại event Handles TextBoxxx.KeyPress
        'Integer
        If Not [Char].IsDigit(e.KeyChar) AndAlso Not [Char].IsControl(e.KeyChar) Then
            e.Handled = True        'Không cho phép gõ
        End If
    End Sub
    Public Sub AllowFloatNumber(sender As Object, e As KeyPressEventArgs)     ' Gọi tại event Handles TextBoxxx.KeyPress
        'Double
        'ChrW(Keys.Enter)
        e.Handled = True    'Không cho phép gõ; Mặc định là false
        If [Char].IsDigit(e.KeyChar) OrElse [Char].IsControl(e.KeyChar) OrElse (e.KeyChar = "-"c) OrElse ((e.KeyChar = "."c) AndAlso Not sender.text.ToString.Contains(".")) Then       'là số hoặc ký tự di chuyển, hoặc (là dấu chấm và chuỗi chưa có dấu chấm thì tiếp tục,có dấu chấm rồi thì end if)
            e.Handled = False    '  Cho phép gõ
        End If
        'If e.KeyChar = "-" Then
        '    sender.text = "-" + sender.text.ToString.Replace("-", "")
        'End If
        'If Not [Char].IsDigit(e.KeyChar) AndAlso Not [Char].IsControl(e.KeyChar) Then
        '    e.Handled = True
        'End If
        '#Region "===================another way"

        '            If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
        '                e.KeyChar = e.KeyChar 'Allows only numbers
        '            ElseIf Asc(e.KeyChar) = 8 Then
        '                e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        '            ElseIf e.KeyChar = " "c Then
        '                e.KeyChar = " "c 'Allows "Spacebar" to be used
        '            ElseIf e.KeyChar = ","c Then
        '                e.KeyChar = ","c
        '            ElseIf e.KeyChar = "." Then
        '                If txtName.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
        '                    e.Handled = True
        '                    Beep()
        '                End If
        '            Else
        '                e.Handled = True  'Disallows all other characters from being used on txtNights.Text
        '                Beep()
        '            End If
        '#End Region


    End Sub
    Public Sub AllowPercent(sender As Object, e As KeyPressEventArgs)     ' Gọi tại event Handles TextBoxxx.KeyPress
        If Not [Char].IsDigit(e.KeyChar) AndAlso Not [Char].IsControl(e.KeyChar) AndAlso (e.KeyChar <> "."c) AndAlso (e.KeyChar <> "-"c) Then
            e.Handled = True
            Return
        End If
        'If e.KeyChar = "-" Then
        '    sender.text = "-" + sender.text.ToString.Replace("-", "")
        '    'sender.text = sender.text.ToString.Replace("-", "")
        'End If
        ''Có giá trị % > 100%, khong, gia tri nao >100 thi chuyen sang Nhom khac
        If sender.text.ToString.Contains("-") AndAlso (e.KeyChar = "-"c) Then   'Nếu lớn hơn 10 thì không cho gõ tiếp vào vị trí trước dấu chấm thập phân (Vị trí nhỏ hơn 3)
            e.Handled = True
            Beep()
        End If
        Try
            If sender.text.length > 0 And Not CType(sender, DevExpress.XtraEditors.TextEdit).SelectionLength > 0 Then 'CType(sender, TextBox).SelectionLength = sender.textlength Then ' để ko cho GTri >100  <-100; sender.selectionglength = sender.textlengthThen=> khi text dc selectall
                If sender.text = "." Then
                    Return
                End If

                If CType(sender.text, Double) > 10 AndAlso sender.SelectionStart < 3 AndAlso (e.KeyChar <> "."c) Then   'Nếu lớn hơn 10 thì không cho gõ tiếp vào vị trí trước dấu chấm thập phân (Vị trí nhỏ hơn 3)
                    e.Handled = True
                    Beep()
                End If
               
                'If CType(sender.text, Double) = 10 AndAlso sender.SelectionStart < 3 AndAlso (e.KeyChar <> "0"c) Then
                '    e.Handled = True
                'End If
                If CType(sender.text, Double) >= 100 Then
                    e.Handled = True
                End If
                If CType(sender.text, Double) < -10 AndAlso (e.KeyChar <> "."c) Then    ' để ko cho GTri ?100
                    e.Handled = True
                    Beep()
                End If
                If CType(sender.text, Double) = -10 AndAlso (e.KeyChar = "0"c) Then
                    e.Handled = False
                End If

                'If e.KeyChar = "-" Then
                '    sender.text = "-" + sender.text.ToString.Replace("-", "")
                '    'sender.text = sender.text.ToString.Replace("-", "")
                'End If

            End If


            ' MessageBox.Show(sender.SelectionStart)


            If e.KeyChar = "." Then
                If sender.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                    e.Handled = True
                    Beep()
                End If
            End If

            If e.KeyChar = Chr(8) Then ' Allow backspace
                e.Handled = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub AllowSpecificKey(sender As Object, e As KeyPressEventArgs)     ' Gọi tại event Handles TextBoxxx.KeyPress
        ''Chỉ cho gõ key T hoặc t để autocomplete tới có hoặc TP, TX; Không cho gõ hơn 1 ký tự
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar <> "T"c AndAlso (e.KeyChar <> "t"c) Or sender.text.length > 0) Then
            e.Handled = True    'Không cho phép gõ
        End If
    End Sub
    Public Sub NumberFormat(ByVal Text As DevExpress.XtraEditors.TextEdit, deciplace As Integer)        ' Gọi tại event Handles TextBoxxx.TextChanged

        If Len(Text.Text) > 0 Then
            Text.Text = FormatNumber(CDbl(Text.Text), deciplace)
            Dim x As Integer = Text.SelectionStart.ToString
            If x = 0 Then
                Text.SelectionStart = Len(Text.Text)
                Text.SelectionLength = 0
            Else
                Text.SelectionStart = x
                Text.SelectionLength = 0
            End If
        End If
    End Sub


End Class
