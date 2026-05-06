using System;
using System.Windows.Forms;

public class GenerateForm : Form
{
    public int Quantity { get; private set; }
    public int Min { get; private set; }
    public int Max { get; private set; }

    private TextBox txtQtd;
    private TextBox txtMin;
    private TextBox txtMax;

    public GenerateForm()
    {
        this.Text = "Gerar Números";
        this.Width = 300;
        this.Height = 250;

        Label lblQtd = new Label() { Text = "Quantidade:", Top = 20, Left = 20 };
        txtQtd = new TextBox() { Top = 40, Left = 20, Width = 200 };

        Label lblMin = new Label() { Text = "Mínimo:", Top = 70, Left = 20 };
        txtMin = new TextBox() { Top = 90, Left = 20, Width = 200 };

        Label lblMax = new Label() { Text = "Máximo:", Top = 120, Left = 20 };
        txtMax = new TextBox() { Top = 140, Left = 20, Width = 200 };

        Button btnOk = new Button()
        {
            Text = "Gerar",
            Top = 170,
            Left = 20
        };

        btnOk.Click += (s, e) =>
        {
            if (!int.TryParse(txtQtd.Text, out int qtd) ||
                !int.TryParse(txtMin.Text, out int min) ||
                !int.TryParse(txtMax.Text, out int max))
            {
                MessageBox.Show("Valores inválidos!");
                return;
            }

            Quantity = qtd;
            Min = min;
            Max = max;

            this.DialogResult = DialogResult.OK;
            this.Close();
        };

        this.Controls.AddRange(new Control[]
        {
            lblQtd, txtQtd,
            lblMin, txtMin,
            lblMax, txtMax,
            btnOk
        });
    }
}