open System
open System.Windows.Forms
open System.Drawing
open InputHandling
open TextAnalysis

// Main application function
[<STAThread>]
let main () =
    // Create the main form
    let form = new Form(Text = "Text Analyzer", Width = 600, Height = 500, BackColor = Color.SteelBlue, Visible=true)

    // Create input TextBox
    let inputBox = new TextBox(Multiline = true, Width = 560, Height = 150, Top = 10, Left = 10, ScrollBars = ScrollBars.Vertical)

    // Create Analyze and Load buttons
    let analyzeButton = new Button(Text = "Analyze", Width = 100, Top = 170, Left = 10)
    let loadButton = new Button(Text = "Load File", Width = 100, Top = 170, Left = 120)

    // Create a TextBox for displaying results
    let outputBox = new TextBox(Multiline = true, Width = 560, Height = 200, Top = 230, Left = 10, ReadOnly = true, ScrollBars = ScrollBars.Vertical)

    // Add controls to the form
    form.Controls.Add(inputBox)
    form.Controls.Add(analyzeButton)
    form.Controls.Add(loadButton)
    form.Controls.Add(outputBox)

    // Event handler for Analyze button
    analyzeButton.Click.Add(fun _ ->
        let inputText = inputBox.Text
        outputBox.Text <- analyzeText inputText
    )

    // Event handler for Load File button
    loadButton.Click.Add(fun _ ->
        let openFileDialog = new OpenFileDialog(Filter = "Text Files (*.txt)|*.txt")
        openFileDialog.InitialDirectory <- @"D:\";
        if openFileDialog.ShowDialog() = DialogResult.OK then
            let filePath = openFileDialog.FileName
            try
                inputBox.Text <- loadTextFromFile filePath
            with
            | :? System.UnauthorizedAccessException ->
              MessageBox.Show("You do not have permission to access this file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
            | ex ->
              MessageBox.Show($"An error occurred while loading the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
    )

    // Run the application
    Application.Run(form)

// Entry point
[<STAThread>] 
[<EntryPoint>]
let mainEntry _ =
    main ()
    0 // Return an integer exit code