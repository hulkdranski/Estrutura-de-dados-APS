using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

public class MainForm : Form
{
    private AppController controller = new AppController();
    private List<int> currentList = new List<int>();

    private TextBox txtInput;
    private TextBox txtOutput;
    private ComboBox cbSort;
    private Label lblTempo;

    private Button btnManual;
    private Button btnGenerate;
    private Button btnSort;
    private Button btnCompare;
    private TextBox txtSearch;
    private Button btnSearch;
    private Label lblSearchResult;
    private Button btnImport;

    public MainForm()
    {
        this.Text = "APS - Estrutura de Dados";
        this.Width = 680;
        this.Height = 420;

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        GroupBox groupInput = new GroupBox()
        {
            Text = "Entrada de Dados",
            Top = 10,
            Left = 10,
            Width = 280,
            Height = 120
        };
        groupInput.Controls.Add(txtInput);
        groupInput.Controls.Add(btnManual);
        groupInput.Controls.Add(btnGenerate);
        groupInput.Controls.Add(btnImport);

        // Entrada manual
        txtInput = new TextBox()
        {
            Top = 30,
            Left = 15,
            Width = 255
        };

        btnManual = new Button()
        {
            Text = "Inserir lista acima",
            Top = 60,
            Left = 15,
            Width = 120
        };

        btnManual.Click += BtnManual_Click;

        // Gerar números
        btnGenerate = new Button()
        {
            Text = "Gerar lista aleatória",
            Top = 90,
            Left = 105
        };

        btnGenerate.Click += BtnGenerate_Click;

        btnImport = new Button()
        {
            Text = "Importar TXT",
            Top = 60,
            Left = 150,
            Width = 120
        };

        btnImport.Click += BtnImport_Click;

        GroupBox groupInput2 = new GroupBox()
        {
            Text = "Ordenação de Dados",
            Top = 130,
            Left = 10,
            Width = 280,
            Height = 110
        };
        groupInput.Controls.Add(btnSort);
        groupInput.Controls.Add(btnCompare);
        groupInput.Controls.Add(cbSort);
        groupInput.Controls.Add(lblTempo);
        
        // Combo de algoritmos
        cbSort = new ComboBox()
        {
            Top = 145,
            Left = 70,
            Width = 150,
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        cbSort.Items.AddRange(new string[]
        {
            "Bubble",
            "Selection",
            "Insertion",
            "Merge",
            "Quick"
        });

        cbSort.SelectedIndex = 0;

        // Botão ordenar
        btnSort = new Button()
        {
            Text = "Ordenar",
            Top = 180,
            Left = 50
        };

        btnSort.Click += BtnSort_Click;

        // Botão comparar
        btnCompare = new Button()
        {
            Text = "Comparar",
            Top = 180,
            Left = 165
        };

        btnCompare.Click += BtnCompare_Click;


        // Label tempo
        lblTempo = new Label()
        {
            Top = 210,
            Left = 15,
            Width = 190,
            Text = "Tempo: "
        };

        GroupBox groupInput3 = new GroupBox()
        {
            Text = "Busca binária",
            Top = 250,
            Left = 10,
            Width = 280,
            Height = 110
        };
        groupInput.Controls.Add(txtSearch);
        groupInput.Controls.Add(btnSearch);
        groupInput.Controls.Add(lblSearchResult);

        txtSearch = new TextBox()
        {
            Top = 270,
            Left = 50,
            Width = 200
        };

        btnSearch = new Button()
        {
            Text = "Buscar elemento acima",
            Top = 300,
            Left = 75,
            Width = 140
        };

        btnSearch.Click += BtnSearch_Click;

        lblSearchResult = new Label()
        {
            Top = 330,
            Left = 15,
            Width = 400,
            Text = "Resultado da busca: "
        };

        txtOutput = new TextBox()
        {
            Top = 20,
            Left = 300,
            Width = 350,
            Height = 340,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical
        };

        this.Controls.AddRange(new Control[]
        {
            txtInput, btnManual,
            btnGenerate,
            cbSort,
            btnSort,
            btnCompare,
            lblTempo,
            txtOutput,
            txtSearch,
            btnSearch,
            lblSearchResult,
            btnImport,
            groupInput,
            groupInput2,
            groupInput3
        });
    }

    // Inserção manual
    private void BtnManual_Click(object sender, EventArgs e)
    {
        try
        {
            currentList = controller.ManualInput(txtInput.Text);
            txtOutput.Text = string.Join(", ", currentList);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    // Gerar números (abre popup)
    private void BtnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            GenerateForm form = new GenerateForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                currentList = controller.GenerateNumbers(form.Quantity, form.Min, form.Max);
                txtOutput.Text = string.Join(", ", currentList);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    // Ordenar com tempo
    private void BtnSort_Click(object sender, EventArgs e)
    {
        try
        {
            if (currentList == null || currentList.Count == 0)
            {
                MessageBox.Show("Lista vazia!");
                return;
            }

            string metodo = cbSort.SelectedItem.ToString();
            SortResult result = null;

            switch (metodo)
            {
                case "Bubble":
                    result = controller.MeasureBubble(currentList);
                    break;

                case "Selection":
                    result = controller.MeasureSelection(currentList);
                    break;

                case "Insertion":
                    result = controller.MeasureInsertion(currentList);
                    break;

                case "Merge":
                    result = controller.MeasureMerge(currentList);
                    break;

                case "Quick":
                    result = controller.MeasureQuick(currentList);
                    break;
            }

            currentList = result.SortedList;

            txtOutput.Text = string.Join(", ", currentList);
            lblTempo.Text = $"Tempo ({metodo}): {result.ElapsedMilliseconds} ms";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    // Abrir comparação
    private bool IsSorted(List<int> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            if (list[i] > list[i + 1])
                return false;
        }
        return true;
    }
    
    private void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (currentList == null || currentList.Count == 0)
            {
                MessageBox.Show("Lista vazia!");
                return;
            }

            // Verifica se está ordenada
            if (!IsSorted(currentList))
            {
                MessageBox.Show("A busca binária só funciona com a lista ordenada!");
                return;
            }

            if (!int.TryParse(txtSearch.Text, out int target))
            {
                MessageBox.Show("Digite um número válido!");
                return;
            }

            // ⏱️ Medir tempo
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int index = controller.SearchBinary(currentList, target);

            sw.Stop();

            if (index >= 0)
            {
                lblSearchResult.Text = 
                    $"Encontrado no índice: {index} | Tempo: {sw.ElapsedMilliseconds} ms";
            }
            else
            {
                lblSearchResult.Text = 
                    $"Elemento não encontrado | Tempo: {sw.ElapsedMilliseconds} ms";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    
    private void BtnImport_Click(object sender, EventArgs e)
    {
        try
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Arquivos TXT (*.txt)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                currentList = controller.ImportFile(dialog.FileName);

                txtOutput.Text = string.Join(", ", currentList);

                lblTempo.Text = "Tempo: ";
                lblSearchResult.Text = "Resultado da busca: ";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao importar arquivo: " + ex.Message);
        }
    }
    
    private void BtnCompare_Click(object sender, EventArgs e)
    {
        if (currentList == null || currentList.Count == 0)
        {
            MessageBox.Show("Lista vazia!");
            return;
        }

        CompareForm form = new CompareForm(currentList);
        form.ShowDialog();
    }
}