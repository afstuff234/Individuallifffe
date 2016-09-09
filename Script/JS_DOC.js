function MyOpen_MS_Word(strDocName) {

    //This example launches a Microsoft Word application and creates a new document with some text content. 


    //myado_conn = Server.CreateObject("ADODB.Connection");
    //myado_conn.Open("Provider=SQLOLEDB;Data Source=ABS;Initial Catalog=ABS-PC;User ID=sa;Password;");
    //myado_conn.close;
    //alert("Open and Close Connection to ADODB Connection OK...");

    var pause = 0;
    var wdDialogFileOpen = 80;
    var strLocation = "c:\\temp\\test1.docx";
    strLocation = strDocName;

    var wordApp = null;

    if (window.ActiveXObject) {
        //try {
            wordApp = new ActiveXObject("Word.Application");
            //wordApp.Visible = true;
            wordApp.Visible = false;

            //ok
            //var mydoc = wordApp.Documents.Add ();
            //var sel = wordApp.Selection;
            //sel.TypeText("Text Content");
            //sel.TypeText("\nMS Word from JavaScript Text Content");

            //ok
            wordApp.Documents.Open(strLocation);

            //copy the content from my word document and throw it into my variable
            var txtdoc;
            txtdoc = wordApp.Documents(strLocation).Content;
            document.getElementById("div_doc").innerHTML = txt;

            //ok
            //var mydoc = wordApp.Documents.Open(strLocation);
            //docText = mydoc.Content;
            //  Print on webpage                 
            //document.write(docText);

            // Do search or find         
            //var findword = "WHAT TO FIND";
            //findword = "Director";
            //var docrange = wordApp.Documents(strLocation).Content; 
            //docrange.Find.Execute(findword);
            //if(docrange.Find.Found){txt = "Search OK. " + findword;}

            //document.all.tbContentElement.DOM.body.innerHTML = txt;
            //document.form1.tbContentElement.DOM.body.innerHTML = txt;


            //ok
            //var dialog = wordApp.Dialogs(wdDialogFileOpen);
            //var button = dialog.Show(pause);                


            // Save the document.
            //wordApp.Documents.Save();
            //wordApp.Documents.Save("c:\\temp\doc_From_javaScript.doc");
            //mydoc.SaveAs("c:\\temp\doc_From_javaScript1.doc");

            // quit word
            wordApp.quit(0);
            wordApp = null;

        //}
        //catch (e) {
        //    alert("Error has occured. Reason: " + e.message);
        //}
    }
    else {
        alert("Your browser does not support running ActiveX Object." +
            "\nNote that the default security settings of the Internet zone do not allow to initialize" +
            "\nthe Microsoft ActiveX control for scripting in Internet Explorer, but it can be" +
            "\nchanged by selecting the following from your browser:" +
            "\n\tTools" +
            "\n\tInternet Options" +
            "\n\tSecurity tab" +
            "\n\tInternet zone or Local Intranet Zone" +
            "\n\tCustom Level button" +
            "\n\tActiveX controls and plug-ins section" +
            "\n\tInitialize and script ActiveX controls not marked as safe for scripting" +
            "\n\nTry re-run the program after you set the security setting." +
            "\nYou and alson Consult your system administrator...")

    }
}
