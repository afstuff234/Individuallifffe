Imports Microsoft.VisualBasic
Imports System.Reflection

Public Class DateValidator
    Shared _rtnMessage As String
    Shared rtnMessage As String

    Public Shared Function ValidateDate(ByVal sentDate As String) As Boolean

        'Validate date
        Dim sentDate_ As String() = Split(sentDate, "/")
        If sentDate_.Count <> 3 Then
            rtnMessage = "Missing or Invalid Expecting full date in dd/mm/yyyy format ..."
            _rtnMessage = "Javascript:alert('" + rtnMessage + "')"
            Return _rtnMessage
            'Exit Function
        End If

        Dim strMyDay = sentDate_(0)
        Dim strMyMth = sentDate_(1)
        Dim strMyYear = sentDate_(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)
        If Val(strMyYear) < 1999 Then
            _rtnMessage = "Javascript:alert('Error. Proposal year date is less than 1999 ...')"
            Return _rtnMessage
        End If

        Dim strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)
        Dim dateResult As Boolean = MOD_GEN.gnTest_TransDate(strMyDte)

        Return dateResult
    End Function

End Class
