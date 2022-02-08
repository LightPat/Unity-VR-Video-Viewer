/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


public class PopupFiles : MonoBehaviour
{

	void Start()
	{
		gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
	}
    
    	void TaskOnClick()
	{
		Process.Start("explorer.exe" , @"C:\Users");
	}
}


public class PopupFiles : MonoBehaviour
{
    private void OpenFolder(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = folderPath,
                FileName = "explorer.exe"
        };

        Process.Start(startInfo);
    }

    }
}

public class OpenFiles : MonoBehaviour
{
    private void button1_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();

        ofd.Filter = "mp4 files (*.mp4)|*.mp4|All Files (*.*)|*.*";

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            textBox1.Text = ofd.FileName;
            richTextBox1.Text = fileOpen.ReadAllText(ofd.FileName);
        } 
    }
}

*/