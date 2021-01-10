Private Class Valores
    Private _Valor_1 As Integer
    Public Property Valor_1() As Integer
        Get
            Return _Valor_1
        End Get
        Set(ByVal value As Integer)
            _Valor_1 = value
        End Set
    End Property

    Private _Valor_2 As Integer
    Public Property Valor_2() As Integer
        Get
            Return _Valor_2
        End Get
        Set(ByVal value As Integer)
            _Valor_2 = value
        End Set
    End Property
End Class
Public Function Sumar_Valores() As Integer
    Try
        Dim valores As Valores
        valores.Valor_1 = 50
        valores.Valor_2 = 100000
        Return Valores.Valor_1 + Valores.Valor_2
    Catch ex As Exception
        MessageBox.Show(Me, "Error: " & ex.Message.ToString)
        Return 0
    End Try
End Function