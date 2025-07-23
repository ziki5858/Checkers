using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

public static class GeneralHelper
{

    
    public static void ShowError(string text) =>
        MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public static DialogResult ShowErrorConfirm(string text) =>
        MessageBox.Show(text, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

  
    public static void ShowInfo(string text) =>
        MessageBox.Show(text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
}
