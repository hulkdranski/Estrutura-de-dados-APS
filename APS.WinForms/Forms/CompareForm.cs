using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public class CompareForm : Form
{
    private AppController controller = new AppController();
    private List<int> originalList;

    private CheckBox cbBubble;
    private CheckBox cbSelection;
    private CheckBox cbInsertion;
    private CheckBox cbMerge;
    private CheckBox cbQuick;

    private Panel panelResults;

    public CompareForm(List<int> list)
    {
        this.originalList = list;

        this.Text = "Comparação de Algoritmos";
        this.Width = 600;
        this.Height = 400;

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        cbBubble = new CheckBox() { Text = "Bubble", Top = 20, Left = 20 };
        cbSelection = new CheckBox() { Text = "Selection", Top = 50, Left = 20 };
        cbInsertion = new CheckBox() { Text = "Insertion", Top = 80, Left = 20 };
        cbMerge = new CheckBox() { Text = "Merge", Top = 110, Left = 20 };
        cbQuick = new CheckBox() { Text = "Quick", Top = 140, Left = 20 };

        Button btnCompare = new Button()
        {
            Text = "Comparar",
            Top = 180,
            Left = 20,
            Width = 100
        };

        btnCompare.Click += BtnCompare_Click;

        panelResults = new Panel()
        {
            Top = 20,
            Left = 150,
            Width = 400,
            Height = 300,
            AutoScroll = true,
            BorderStyle = BorderStyle.FixedSingle
        };

        this.Controls.AddRange(new Control[]
        {
            cbBubble, cbSelection, cbInsertion, cbMerge, cbQuick,
            btnCompare, panelResults
        });
    }

    private void BtnCompare_Click(object sender, EventArgs e)
    {
        panelResults.Controls.Clear();

        if (originalList == null || originalList.Count == 0)
        {
            MessageBox.Show("Lista vazia!");
            return;
        }

        var resultados = new List<(string nome, long tempo)>();

        if (cbBubble.Checked)
            resultados.Add(("Bubble", controller.MeasureBubble(originalList).ElapsedMilliseconds));

        if (cbSelection.Checked)
            resultados.Add(("Selection", controller.MeasureSelection(originalList).ElapsedMilliseconds));

        if (cbInsertion.Checked)
            resultados.Add(("Insertion", controller.MeasureInsertion(originalList).ElapsedMilliseconds));

        if (cbMerge.Checked)
            resultados.Add(("Merge", controller.MeasureMerge(originalList).ElapsedMilliseconds));

        if (cbQuick.Checked)
            resultados.Add(("Quick", controller.MeasureQuick(originalList).ElapsedMilliseconds));

        if (resultados.Count == 0)
        {
            MessageBox.Show("Selecione pelo menos um algoritmo!");
            return;
        }


        long menor = long.MaxValue;

        foreach (var r in resultados)
        {
            if (r.tempo < menor)
                menor = r.tempo;
        }

        // 👇 Depois continua normal
        long maxTempo = 1;
        foreach (var r in resultados)
        {
            if (r.tempo > maxTempo)
                maxTempo = r.tempo;
        }

        int top = 10;

        foreach (var r in resultados)
        {
            AddBar(r.nome, r.tempo, maxTempo, menor, top);
            top += 40;
        }
    }

    private void AddBar(string nome, long tempo, long maxTempo, long menor, int top)
    {
        int maxWidth = 250;
        int largura = (int)((tempo / (double)maxTempo) * maxWidth);

        // Texto
        Label lbl = new Label()
        {
            Text = tempo == menor 
                ? $"{nome}: {tempo} ms"
                : $"{nome}: {tempo} ms",
            Top = top,
            Left = 10,
            Width = 140
        };

        // Barra
        Panel bar = new Panel()
        {
            Top = top,
            Left = 140,
            Width = Math.Max(largura, 5),
            Height = 20,
            BackColor = GetColor(nome)
        };

        panelResults.Controls.Add(lbl);
        panelResults.Controls.Add(bar);
    }

    private Color GetColor(string nome)
    {
        return nome switch
        {
            "Bubble" => Color.Red,
            "Selection" => Color.Orange,
            "Insertion" => Color.Gold,
            "Merge" => Color.Green,
            "Quick" => Color.Blue,
            _ => Color.Gray
        };
    }
}