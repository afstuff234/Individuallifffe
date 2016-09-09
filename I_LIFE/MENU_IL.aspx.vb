
Partial Class I_LIFE_MENU_IL
    Inherits System.Web.UI.Page

    Protected PageLinks As String

    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    Protected BufferStr As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.txtAction.Text = "Log_Out" Then
            Call DoProc_LogOut()
            Me.txtAction.Text = ""
            'Response.Redirect("LoginP.aspx")
            'Response.Redirect(Request.ApplicationPath & "~/Default.aspx")
            Response.Redirect(Request.ApplicationPath & "~/m_menu.aspx?menu=home")
            Exit Sub
        End If


        'Put user code to initialize the page here
        Dim mKey As String
        Try
            mKey = Page.Request.QueryString("menu").ToString
            'mkey options = MKT UND CLM REIN CRCO ACC ADMIN
        Catch
            mKey = "HOME"
        End Try

        STRMENU_TITLE = "+++ Home Page +++ "
        BufferStr = ""

        'Me.LNK_CODE.Enabled = True
        'Me.LNK_QUOTE.Enabled = True
        'Me.LNK_UND.Enabled = True
        'Me.LNK_ENDORSE.Enabled = True
        'Me.LNK_PROCESS.Enabled = True
        'Me.LNK_CLP.Enabled = True
        'Me.LNK_REINS.Enabled = True

        'Me.LNK_CODE.BackColor = Drawing.Color.White
        'Me.LNK_QUOTE.BackColor = Drawing.Color.White
        'Me.LNK_UND.BackColor = Drawing.Color.White
        'Me.LNK_ENDORSE.BackColor = Drawing.Color.White
        'Me.LNK_PROCESS.BackColor = Drawing.Color.White
        'Me.LNK_CLP.BackColor = Drawing.Color.White
        'Me.LNK_REINS.BackColor = Drawing.Color.White

        Call DoProc_Menu(mKey)

    End Sub

    Private Sub DoProc_Menu(ByVal pvMenu As String)
        Select Case pvMenu.ToUpper
            Case "HOME"
                STRMENU_TITLE = "+++ Individual Life Module +++ "
                'AddMenuItem("", "Welcome to ABS Web Accounts Manager", "MainM.aspx?menu=HOME")
                BufferStr += "<tr>"
                BufferStr += "<td align='center' valign='top'>"
                BufferStr += "&nbsp;<img alt='Image' src='../Images/Family.jpg' style='width: 850px; height: 450px' />&nbsp;"
                BufferStr += "</td>"
                BufferStr += "</tr>"

            Case "GEN"
                STRMENU_TITLE = "+++ Parameters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Company Data Setup", "javascript:jsDoPopNew_Full('General/GEN110.aspx?TTYPE=COY')")
                AddMenuItem("", "Server Parameters Setup", "menu_il.aspx?menu=GEN")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")

            Case "IL_CODE"
                'Me.LNK_CODE.BackColor = Drawing.Color.Teal
                'Me.LNK_CODE.Font.Bold = True
                STRMENU_TITLE = "+++ Master Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Master Setup", "Codes File Setup", "menu_il.aspx?menu=il_code_std")

                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Customer Masters Setup", "menu_il.aspx?menu=il_code_cust")
                'AddMenuItem("", "Master Codes Setup", "menu_il.aspx?menu=il_code_mst")

                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Rate Masters", "menu_il.aspx?menu=il_code_rate")
                AddMenuItem("", "Product Master Setup", "menu_il.aspx?menu=il_code_prod")

                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Underwriting Codes Setup", "menu_il.aspx?menu=il_code_und")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Other Underwriting Setup", "menu_il.aspx?menu=il_code_rate")

                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Reinsurance Codes Setup", "menu_il.aspx?menu=il_code_reins")
                'AddMenuItem("", "", "") 'blank link

                '<br>
                '<a href="http://localhost/abslife/I_LIFE/MENU_IL.aspx?menu=IL_CODE">
                '	Return to Previous Menu...
                '</a>

                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Other Underwriting Codes Setup", "http://custodia-hdpo9m/IndLife/EntryMenu.aspx")
                'AddMenuItem("", "Other Underwriting Codes Setup", "http://192.168.10.18/IndLife/EntryMenu.aspx")
                'AddMenuItem("", "", "") 'blank link

                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
            Case "IL_CODE_STD"
                STRMENU_TITLE = "+++ Master Setup >>> Codes File Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Codes File Setup", "Nationality Codes Setup", "PRG_LI_CODES.aspx?optid=001&optd=Nationality")
                AddMenuItem("", "State Codes Setup", "PRG_LI_CODES.aspx?optid=002&optd=State")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Branch Codes Setup", "PRG_LI_CODES.aspx?optid=003&optd=Branch")
                AddMenuItem("", "Division Codes Setup", "PRG_LI_CODES.aspx?optid=004&optd=Division")
                AddMenuItem("", "Department Codes Setup", "PRG_LI_CODES.aspx?optid=005&optd=Department")
                AddMenuItem("", "Location Codes Setup", "PRG_LI_CODES.aspx?optid=006&optd=Location")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Occupation Class Setup", "PRG_LI_CODES.aspx?optid=007&optd=Occupation_Class")
                AddMenuItem("", "Occupation Codes Setup", "PRG_LI_CODES.aspx?optid=008&optd=Occupation")
                AddMenuItem("", "Religion/Belief Codes Setup", "PRG_LI_CODES.aspx?optid=009&optd=Religion")
                AddMenuItem("", "Customer Title Codes Setup", " PRG_LI_CODES.aspx?optid=010&optd=Customer_Title")
                AddMenuItem("", "Rider Codes Setup", "PRG_LI_CODES.aspx?optid=011&optd=Rider")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Charge Codes Setup", "PRG_LI_CODES.aspx?optid=012&optd=Charge_Codes")
                AddMenuItem("", "Relation Codes Setup", "PRG_LI_CODES.aspx?optid=013&optd=Relation")
                AddMenuItem("", "Source of Business Codes Setup", "PRG_LI_CODES.aspx?optid=014&optd=Source_Of-Business")
                AddMenuItem("", "Gender Codes Setup", "PRG_LI_CODES.aspx?optid=015&optd=Gender")
                AddMenuItem("", "Agency Location Setup", "PRG_LI_CODES.aspx?optid=016&optd=Agency_Location")
                AddMenuItem("", "Currency Codes Setup", "PRG_LI_CODES.aspx?optid=017&optd=Currency_Codes")
                AddMenuItem("", "Marital Status", "PRG_LI_CODES.aspx?optid=020&optd=Marital_Status")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_code")
            Case "IL_CODE_CUST"
                STRMENU_TITLE = "+++ Master Setup >>> Customer Masters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Customer Masters", "Customer Class Setup", "PRG_LI_CUST_CLASS.aspx?optid=001&optd=Customer_Category")
                AddMenuItem("", "Customer Details (Assured Details)", "PRG_LI_CUST_DTL.aspx?optid=001&optd=Customer_Details")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Agents/Brokers Category Setup", "PRG_LI_BRK_CAT.aspx?optid=001&optd=Brokers_Agents_Category")
                AddMenuItem("", "Agency/Brokers Details", "PRG_LI_BRK_DTL.aspx?optid=001&optd=Brokers_Agents_Details")
                AddMenuItem("", "Marketers Codes Setup (Agencies)", "PRG_LI_MKT_CD.aspx?optid=001&optd=Marketers_Agency")

                AddMenuItem("", "Agent Profile", "PRG_LI_AGENT_PROFILE.aspx")
                'AddMenuItem("", "Agent Hierarchy", "PRG_LI_AGENT_HIERARCHY.aspx")

                AddMenuItem("", "Contractor Details", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_code")
            Case "IL_CODE_MST"
            Case "IL_CODE_PROD"
                STRMENU_TITLE = "+++ Master Setup >>> Product Master Codes Setup Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Products", "Product Category/Class Setup", "")
                AddMenuItem("", "Product Details", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Cover Master", "")
                AddMenuItem("", "Plan Master", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_code")

            Case "IL_CODE_RATE"
                STRMENU_TITLE = "+++ Master Setup >>> Other Underwriting Codes Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Other Underwriting Codes", "Medical Exam Details Setup", "")
                AddMenuItem("", "Rate Type/Category Setup", "")
                AddMenuItem("", "Rate Master Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Allocation to Investment Rate Setup", "")
                AddMenuItem("", "Investment Charge Setup", "")
                AddMenuItem("", "Interest Rate Setup", "")
                AddMenuItem("", "Bonus Interest Rate Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Loan Interest Rate Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Agency Standard Commission Rate Setup", "")
                AddMenuItem("", "Agency Production Budget Setup", "PRG_LI_AGCY_BUDGET.aspx")
                AddMenuItem("", "Agency Commission Rate Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_code")

            Case "IL_CODE_UND"
                STRMENU_TITLE = "+++ Master Setup >>> Underwriting Codes Masters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Underwriting Codes", "Underwriting Codes Setup", "PRG_LI_UNDW_CODE_ENQ.aspx")
                'AddMenuItem("Underwriting Codes", "Disability Type Setup", "PRG_LI_UNDW_CODE_ENQ.aspx?optid=1400&optd=Disability")
                'AddMenuItem("", "Health Status Setup", "")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Medical Illness Codes Setup", "")
                'AddMenuItem("", "Medical Examination Codes Setup", "")
                'AddMenuItem("", "Medical Clinic Details Setup", "")
                'AddMenuItem("", "Mortality Codes Setup", "")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Loading Codes Setup", "")
                'AddMenuItem("", "Discount Codes Setup", "")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Policy Condition Codes Setup", "")
                'AddMenuItem("", "Loss Types Codes Setup", "")
                'AddMenuItem("", "Loan Codes Setup", "")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("Other Underwriting Codes", "Underwriting Codes Setup", "PRG_LI_UNDW_CODE_ENQ.aspx")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il-code")

            Case "IL_CODE_REINS"
                STRMENU_TITLE = "+++ Master Setup >>> Reinsurance Codes Masters Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_code")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "MENU CAPTION", "PAGE URL")
                AddMenuItem("Reinsurance Codes", "ReAssurer Company Category (Local, Overseas)", "il_1510.aspx")
                AddMenuItem("", "ReAssurer Company Codes Setup", "")
                AddMenuItem("", "ReAssurer Treaty Proportion Setup (Yearly Treaty Arrangement", "")
                AddMenuItem("", "ReAssurer Treaty Commission Setup", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il-code")

            Case "IL_QUOTE"
                STRMENU_TITLE = "+++ Proposal Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Proposal", "New Proposal", "PRG_LI_INV_PLUS.aspx")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                'AddMenuItem("", "New Proposal - New Format", "PRG_LI_PROP_HEADER.aspx")
                'AddMenuItem("New Proposal", "Investment Product", "menu_il.aspx?menu=IL_QUOTE_INVEST_PLUS")
                'AddMenuItem("", "Protection", "menu_il.aspx?menu=IL_QUOTE_PROTECTION")
                'AddMenuItem("", "Endowment", "menu_il.aspx?menu=IL_QUOTE_ENDOWMENT")
                'AddMenuItem("", "Funeral", "menu_il.aspx?menu=IL_QUOTE_FUNERAL")

                'AddMenuItem("Proposal", "New Proposal", "PRG_LI_PROP_POLICY.aspx")

                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Convert Proposal to Policy", "")
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("Reports", "Proposal Status Report", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")

            Case "IL_QUOTE_INVEST_PLUS"
                STRMENU_TITLE = "+++ Proposal >>> Investment Product Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_quote")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Investment Product", "Investment Plus", "PRG_LI_INV_PLUS.aspx")
                AddMenuItem("", "Personal Provident", "")
                AddMenuItem("", "Capital Builder", "")
                'AddMenuItem("", "Capital Builder Without Life", "")
                AddMenuItem("", "Dollar Linked", "")
                AddMenuItem("", "Unit Link Plan", "")
                AddMenuItem("", "Esusu Shield", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_quote")
            Case "IL_QUOTE_PROTECTION"
                STRMENU_TITLE = "+++ Proposal >>> Protection Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_quote")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Protection", "Term Assurance", "")
                AddMenuItem("", "Credit Life Capital Sum Option", "")
                AddMenuItem("", "Mortgage Protection", "")
                AddMenuItem("", "Whole Life", "")
                'AddMenuItem("", "The Royal Family Support", "")
                'AddMenuItem("", "Term Assurance - Single Premium", "")
                AddMenuItem("", "Credit Life Outstanding Option", "")
                AddMenuItem("", "Mortgage Protection - Old Rate", "")
                'AddMenuItem("", "Term Assurance - Old Rate", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_quote")
            Case "IL_QUOTE_ENDOWMENT"
                STRMENU_TITLE = "+++ Proposal >>> Endowment Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_quote")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Endowment", "Multi-Endowment Assurance", "")
                AddMenuItem("", "Life Time Harvest", "")
                AddMenuItem("", "Endowment With Profit", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Education Endowment Assurance", "")
                'AddMenuItem("", "Education Endowment Assurance - FEE", "")
                'AddMenuItem("", "Education Endowment Assurance - SA", "")
                'AddMenuItem("", "Education Endowment Assurance - PREMIUM", "")
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Endowment With Profit - Old Rate", "")
                AddMenuItem("", "Endowment Without Profit", "")
                'AddMenuItem("", "Life Term Harvest - SA From Premium", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_quote")
            Case "IL_QUOTE_FUNERAL"
                STRMENU_TITLE = "+++ Proposal >>> Funeral Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_quote")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Funeral", "Funeral Plan", "")
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Funeral Plan Single Premium", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_quote")

            Case "IL_UND"
                'Me.LNK_UND.BackColor = Drawing.Color.Teal
                'Me.LNK_UND.Font.Bold = True
                STRMENU_TITLE = "+++ Underwriting Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("New Policy", "Investment Plan", "menu_il.aspx?menu=IL_UND_INVEST_PLUS")
                'AddMenuItem("", "Protection", "menu_il.aspx?menu=IL_UND_PROTECTION")
                'AddMenuItem("", "Endowment", "menu_il.aspx?menu=IL_UND_ENDOWMENT")
                ''AddMenuItem("", "Funeral", "menu_il.aspx?menu=IL_UND_FUNERAL")
                ''AddMenuItem("", "", "") 'blank link
                ''AddMenuItem("", "Convert Proposal to Policy", "il_3010.aspx")
                ''AddMenuItem("", "New Policy", "il_3020.aspx")
                'AddMenuItem("", "", "") 'blank link
                AddMenuItem("Reports", "New Entrants Report", "PRG_NEW_ENTRANT_RPT.aspx")
                'AddMenuItem("", "Policies Agents Report", "PRG_POLICIES_AGENTS_RPT.aspx") Only Individual


                Dim UserRole As String = ""
                Try
                    UserRole = CType(Session("MyUserRole"), String)
                Catch ex As Exception
                    UserRole = ""
                End Try

                If UserRole = "Administrator" Or UserRole = "Data Manager" Then
                    AddMenuItem("", "Policies Agents Report", "RPT_LI_POLICY_AGENT.aspx") 'Include both Annuity and Individual
                End If

                AddMenuItem("", "Tech Master Dump Reports", "RPT_LI_TECH_REPORTS.aspx") 'Include Tech Master Dump report,Agency Business report and Medical underwriting report
                AddMenuItem("", "Commission Rebate Report", "RPT_LI_COMM_REBATE_REPORTS.aspx")
                AddMenuItem("", "Claims Weekly Report", "RPT_LI_CLAIM_WEEKLY_REPORT.aspx")
                AddMenuItem("", "Weekly Business Report", "RPT_LI_BUSINESS_REPORT.aspx")
                AddMenuItem("", "Maturity List Report", "PRG_LI_INDV_REPORTS.ASPX")
                AddMenuItem("", "Agency Codes Report", "RPT_LI_AGENCY_CODES.aspx")
                AddMenuItem("", "Agency Details", "RPT_LI_AGENCY_DET.aspx")
                AddMenuItem("", "Client Policy Enquiry", "PRG_LI_INDV_ENQUIRY.aspx")
                'AddMenuItem("", "Reinsurance List Report", "RPT_LI_REINS_CERT.aspx")
                AddMenuItem("", "", "") 'blank link

                'AddMenuItem("", "Definite Certificate", "RPT_LI_DEFINITE_CERT.aspx")
                AddMenuItem("", "Proposal Status Report", "")
                AddMenuItem("", "Endorsement Report", "")
                AddMenuItem("", "Letter of Acceptance", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Statement of Account", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Mortgage Protection Certificate", "")
                AddMenuItem("", "Premium Register", "")
                AddMenuItem("", "Premium Account Position Report", "")
                AddMenuItem("", "Reinstated Policies", "")
                AddMenuItem("", "Movement of Life Policies", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Premium Arrears Report", "")
                AddMenuItem("", "Outstanding Premium Letter", "")
                AddMenuItem("", "NAICOM Premium Account Position Report", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Maturity List Report", "PRG_LI_INDV_REPORTS.ASPX")
                AddMenuItem("", "Renewal List Report", "")
                AddMenuItem("", "Renewal Notices Report", "")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Policy Schedule", "")
                'AddMenuItem("", "Policy Register", "")
                'AddMenuItem("", "Production Report", "")
                'AddMenuItem("", "Payment of Medical Fee", "")
                'AddMenuItem("", "Summary of Additional Covers", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Loan Processing", "Loan Request Entry", "")
                AddMenuItem("", "Loan Disbursement Process", "")
                AddMenuItem("", "Loan Repayment Process", "")
                AddMenuItem("", "Loans Report", "")
                AddMenuItem("", "Loans Statement", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                'AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")

                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "", "") 'blank link
                AddMenuItem("Medical Examination", "Medical Underwriting Requirement", "PRG_LI_INDV_MEDICAL_UNDERWRITING.aspx")
                AddMenuItem("", "Medical Examination List", "PRG_LI_INDV_MEDICAL_EXAM_LIST.aspx")

                'AddMenuItem("", "", "") 'blank link


            Case "IL_UND_INVEST_PLUS"
                STRMENU_TITLE = "+++ Underwriting >>> Investment Plan Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_und")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Investment Plan", "Investment Plus", "")
                AddMenuItem("", "Personal Provident", "")
                AddMenuItem("", "Capital Builder", "")
                AddMenuItem("", "Capital Builder Without Life", "")
                AddMenuItem("", "Dollar Linked", "")
                AddMenuItem("", "Unit Link Plan", "")
                AddMenuItem("", "Esusu Shield", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_und")
            Case "IL_UND_PROTECTION"
                STRMENU_TITLE = "+++ Underwriting >>> Protection Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_und")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Protection", "Term Assurance", "")
                AddMenuItem("", "Credit Life Capital Sum Option", "")
                AddMenuItem("", "Mortgage Protection", "")
                AddMenuItem("", "Whole Life", "")
                AddMenuItem("", "The Royal Family Support", "")
                AddMenuItem("", "Term Assurance - Single Premium", "")
                AddMenuItem("", "Credit Life Outstanding Option", "")
                AddMenuItem("", "Mortgage Protection - Old Rate", "")
                AddMenuItem("", "Term Assurance - Old Rate", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_und")
            Case "IL_UND_ENDOWMENT"
                STRMENU_TITLE = "+++ Underwriting >>> Endowment Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_und")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Endowment", "Multi-Endowment Assurance", "")
                AddMenuItem("", "Life Time Harvest", "")
                AddMenuItem("", "Endowment With Profit", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Education Endowment Assurance - FEE", "")
                AddMenuItem("", "Education Endowment Assurance - SA", "")
                AddMenuItem("", "Education Endowment Assurance - PREMIUM", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Endowment With Profit - Old Rate", "")
                AddMenuItem("", "Life Term Harvest - SA From Premium", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_und")
            Case "IL_UND_FUNERAL"
                STRMENU_TITLE = "+++ Underwriting >>> Funeral Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_und")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Funeral", "Funeral Plan", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Funeral Plan - Single Premium", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=il_und")

            Case "IL_ENDORSE"
                'Me.LNK_ENDORSE.BackColor = Drawing.Color.Teal
                'Me.LNK_ENDORSE.Font.Bold = True
                STRMENU_TITLE = "+++ Endorsement Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Operations", "Premium Details Changes", "")
                AddMenuItem("", "Personal Details Changes", "")
                AddMenuItem("", "Premium Details Changes", "")
                AddMenuItem("", "Beneficiary Details Changes", "")
                AddMenuItem("", "Terms/Duration Changes", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
            Case "IL_PROCESS"
                'Me.LNK_PROCESS.BackColor = Drawing.Color.Teal
                'Me.LNK_PROCESS.Font.Bold = True
                STRMENU_TITLE = "+++ Processing Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("Operations", "Processing Lapse Policy", "")
                'AddMenuItem("", "Premium Account Position", "")
                'AddMenuItem("", "Policy Revival Processing", "")
                'AddMenuItem("", "Year End Actuarial Processing", "")
                'AddMenuItem("", "Monthly Commission Processing", "")
                'AddMenuItem("", "Monthly Commission Supplementary", "")
                'AddMenuItem("", "Restore Back Commission Calculation", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Claims Process", "Policy Maturity Process", "")
                AddMenuItem("", "Partial Maturity Process", "")
                AddMenuItem("", "Surrender Policy Process", "")
                AddMenuItem("", "Death Claims Process", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Disability - Critical Illness Claims Process", "")
                AddMenuItem("", "Disability - Accident Claims Process (AFAB)", "")
                AddMenuItem("", "Waver of Premium Process", "")
                AddMenuItem("", "Paid-Up Policies Process", "")
                AddMenuItem("", "Cancelled Policies Process", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Lapse Policies Process", "")
                AddMenuItem("", "Reactivate Cancelled Policies Process", "") 'blank link
                AddMenuItem("", "Partial Withdrawal Process", "")
                AddMenuItem("", "Full Withdrawal Process", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")

            Case "IL_CLAIM"
                'Me.LNK_CLP.BackColor = Drawing.Color.Teal
                'Me.LNK_CLP.Font.Bold = True
                STRMENU_TITLE = "+++ Claims Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "MENU CAPTION", "PAGE URL")
                'AddMenuItem("Transactions", "New Claims Notification/Registration Entry", "")
                'AddMenuItem("", "Claims Settlement", "")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("Operations", "Policy Maturity Process", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Entries", "Claim Request Entry", "PRG_LI_REQ_ENTRY.aspx?optid=NEW")
                AddMenuItem("", "Waiver of Premium", "PRG_LI_CLM_WAIVER.aspx?optid=NEW")
                AddMenuItem("", "Paid-Up Policies", "PRG_PAIDUP_PROCESS.aspx?optid=NEW")
                AddMenuItem("", "Lapse Policies", "PRG_LI_LAPSE_PROCESS.aspx?optid=NEW")
                AddMenuItem("", "Policy Cancellation Process", "PRG_LI_CANCEL_PROCESS.aspx?optid=NEW")
                AddMenuItem("", "Policy Reactivation Process", "PRG_LI_REVIVE_POLICY.aspx?optid=NEW")
                AddMenuItem("", "Maturity Claim Process", "PRG_LI_CLM_MATURE.aspx?optid=NEW")
                AddMenuItem("", "Partial Maturity Claim Process", "PRG_LI_CLM_PART_MATURE.aspx?optid=NEW")

                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Reports", "List of Surrendered Policies", "")
                AddMenuItem("", "List of Paid-Up Policies", "")
                AddMenuItem("", "List of Policies Maturiting", "")
                AddMenuItem("", "List of Lapsed Policies", "")
                AddMenuItem("", "List of Cancelled Policies", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Maturity/Withdrawal", "Maturity Report", "")
                AddMenuItem("", "Partial Maturity Report", "")
                AddMenuItem("", "Partial Withdrwal Report", "")
                AddMenuItem("", "Full Withdrawal Report", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "NAICOM Outstanding Report", "")
                'AddMenuItem("", "Death Claims Report", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")

            Case "IL_REINS"
                'Me.LNK_REINS.BackColor = Drawing.Color.Teal
                'Me.LNK_REINS.Font.Bold = True
                STRMENU_TITLE = "+++ Reinsurance Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "MENU CAPTION", "PAGE URL")
                AddMenuItem("Set up", "ReInsurance Settings", "../REINSURANCE/PRG_LI_REINS_SETTINGS.aspx")
                AddMenuItem("", "ReInsurance Data Entry", "../REINSURANCE/PRG_LI_REINSURANCE.aspx")
                '"~/m_menu.aspx?menu=home")
                AddMenuItem("", "Associated Companies", "PRG_LI_ASSOCIATE_COY.aspx")
                AddMenuItem("", "", "") 'blank link


                AddMenuItem("Transactions", "ReAssurer Data Registration", "")
                AddMenuItem("", "", "")

                'AddMenuItem("", "ReAssurer Facultative Entry", "")
                AddMenuItem("Reports", "Definite Certificate", "RPT_LI_DEFINITE_CERT.aspx")
                AddMenuItem("", "Reinsurance List Report", "RPT_LI_REINS_CERT.aspx")
                AddMenuItem("", "ReAssurer Data Register", "")
                AddMenuItem("", "ReAssurer Bordeareaux", "")
                AddMenuItem("", "Definite Certificate", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")


            Case "IL_SEC"
                STRMENU_TITLE = "+++ Administration Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Administrator Password Change", "")
                AddMenuItem("", "Create New User", "")
                AddMenuItem("", "User Password Change", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")


            Case "IL_RENEWAL"
                STRMENU_TITLE = "+++ Individual Renewal Scheme +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("Renewal", "Pre-Renewal Processing", "")
                AddMenuItem("", "Renewal Scheme", "PRG_LI_POLICY_RENEW.ASPX")




        End Select

    End Sub

    Private Sub AddMenuItem(ByVal LeadItem As String, ByVal MenuItemText As String, ByVal LinkUrl As String)
        Dim myURL As String
        myURL = LinkUrl
        If Trim(myURL) = "" Then
            myURL = "'#'"
        Else
            myURL = "'" & myURL & "'"
        End If

        BufferStr += "<tr>"
        If LeadItem.Trim() = "" Then
            BufferStr += "<td nowrap align='left' valign='top'>&nbsp;&nbsp;</td>"
        Else
            BufferStr += "<td nowrap align='left' valign='top' class='a_sub_menu'>&nbsp;"
            BufferStr += "<img alt='Menu Image' src='../Images/ballred.gif' class='MY_IMG_LINK' />&nbsp;"
            BufferStr += LeadItem & "&nbsp;</td>"
        End If

        If MenuItemText.Trim = "" Then
            BufferStr += "<td nowrap align='left' valign='top'>&nbsp;</td>"
        ElseIf MenuItemText.Trim = "UNDER_LINE" Then
            BufferStr += "<td nowrap align='left' valign='top' class='td_under_line'>&nbsp;</td>"
        ElseIf MenuItemText.Trim = "Returns to Previous Page" Then
            BufferStr += "<td nowrap align='left' valign='bottom' class='td_return_menu'>"
            BufferStr += "<a href=" & myURL & " valign='top' class='a_sub_menu'>" & MenuItemText & "</a>"
            BufferStr += "</td>"
            'a_return_menu
        Else
            BufferStr += "<td nowrap align='left' valign='top' class='td_sub_menu2'>"
            BufferStr += "<img alt='Menu Image' src='../Images/ballredx.gif' class='MY_IMG_LINK' />&nbsp;"
            BufferStr += "<a href=" & myURL & " valign='top' class='a_sub_menu2'>" & MenuItemText & "</a>"
            BufferStr += "</td>"

            'td_sub_menu2
            'a_sub_menu2

            'td_under_line
        End If
        BufferStr += "</tr>"
    End Sub

    Protected Sub DoProc_LogOut()

        Dim strSess As String = ""
        Dim intC As Integer = 0
        Dim intCX As Integer = 0
        Dim MyArray(15) As String

        intC = 0
        intCX = 0
        Try
            'Session("STFID") = RTrim(Me.txtNum.Text)
            'Session("STFID") = RTrim("")

            'Session.Keys
            'Session.Count
            'LOOP THROUGH THE SESSION OBJECT
            '*******************************

            'For intC = 0 To Session.Count - 1
            'Response.Write("<br />" & "Item " & intC & " - Key: " & Session.Keys(intC).ToString & " - Value: : " & Session.Item(intC).ToString)
            '
            'Next

            'SAMPLE SESSION DATA
            '*******************
            ''Item 0 - Key: ActiveSess - Value: : 1
            ''Item 1 - Key: StartdDate - Value: : 06/14/2013 7:01:55 PM
            ''Item 2 - Key: connstr - Value: : Provider=SQLOLEDB;Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 3 - Key: connstr_SQL - Value: : Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 4 - Key: CL_COMP_NAME - Value: : CUSTODIAN AND ALLIED INSURANCE PLC
            ''Item(5 - Key) : MyUserIDX(-Value) : ADM()
            ''Item(6 - Key) : MyUserName(-Value) : System(Administrator)
            ''Item 7 - Key: MyUserLevelX - Value: : 0
            ''Item(8 - Key) : MyUserIDX_NIM(-Value) : ADM()
            ''Item(9 - Key) : MyUserName_NIM(-Value) : System(Administrator)
            ''Item 10 - Key: MyUserLevelX_NIM - Value: : 0
            ''Item 11 - Key: MyLogonTime - Value: : 06/14/2013 7:02:05 PM
            ''Item(12 - Key) : MyUserID(-Value) : ADM()


            'For Each strS As String In Session.Keys
            '    '    ' ...
            '    'Response.Write("<br /> Session ID: " & Session.SessionID & " - Key: " & strSess.ToString & " - Value: " & Session.Item(strSess).ToString)

            '    '    If UCase(strSess) = UCase("connstr") Or _
            '    '      UCase(strSess) = UCase("connstr_SQL") Or _
            '    '      UCase(strSess) = UCase("CL_COMP_NAME") Then
            '    '    Else
            '    '        'Session.Remove(strSess)
            '    '    End If
            '    strSess = strS
            '    Response.Write("<br />" & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
            'Next

            For intCX = 0 To Session.Count - 1

                strSess = Session.Keys(intCX).ToString

                If UCase(strSess) = UCase("connstr") Or _
                  UCase(strSess) = UCase("connstr_SQL") Or _
                  UCase(strSess) = UCase("CL_COMP_NAME") Or _
                  UCase(strSess) = UCase("ActiveSess") Or _
                  UCase(strSess) = UCase("StartdDate") Then
                Else
                    intC = intC + 1
                    MyArray(intC) = strSess
                    'Response.Write("<br />" & "Item " & intC & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)

                End If

            Next

            'Response.Write("<br />" & "Item ubound(): " & UBound(MyArray).ToString)
            'Response.Write("<br />" & "Item Length: " & MyArray.Length)

            For intCX = 1 To intC

                strSess = MyArray(intCX).ToString

                Response.Write("<br />" & "Removing session Item " & intCX & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
                Session.Remove(strSess.ToString)
                'Session.Contents.Remove(strSess)

            Next

            'Session.Clear()

        Catch ex As Exception
            Response.Write("<br /> Error has Occured at key: " & strSess.ToString & " Reason: " & ex.Message.ToString)
            'Exit Try
        End Try


    End Sub


End Class
