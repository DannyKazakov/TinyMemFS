namespace TinyMemFS
{
    public partial class Form1 : Form
    {
        private TinyMemFS MyTinyFS;
        private int numberOfFiles, sizeOfFS ;
        public Form1()
        {
            InitializeComponent();
            listView1.Columns.Clear();
            listView1.Columns.Add("");
            listView1.View = View.Details;
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            MyTinyFS = new TinyMemFS();
            numberOfFiles = 0;
            sizeOfFS = 0;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            buttonComboBox1.Enabled = true;
            int option = comboBox1.SelectedIndex;
            switch (option)
            {
                case 0:
                    buttonComboBox1.Text = "Remove";
                    labelComboBox1.Text = "FileName:";
                    break;
                case 1:
                    buttonComboBox1.Text = "Encrypt";
                    labelComboBox1.Text = "Insert Key:";
                    break;
                case 2:
                    buttonComboBox1.Text = "Decrypt";
                    labelComboBox1.Text = "Insert Key:";
                    break;
                case 3:
                    buttonComboBox1.Text = "Compress";
                    labelComboBox1.Text = "FileName:";
                    break;
                case 4:
                    buttonComboBox1.Text = "Uncompress";
                    labelComboBox1.Text = "FileName:";
                    break;
                default:
                    buttonComboBox1.Enabled = false;
                    break;
            }
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            buttonComboBox2.Enabled = true;
            int option = comboBox2.SelectedIndex;
            switch (option)
            {
                case 0:
                    buttonComboBox2.Text = "Add";
                    labelComboBox2_A.Text = "Insert name:";
                    labelComboBox2_B.Text = "Path of file:";
                    break;
                case 1:
                    buttonComboBox2.Text = "Save";
                    labelComboBox2_A.Text = "File name:";
                    labelComboBox2_B.Text = "Path of file:";
                    break;
                case 2:
                    buttonComboBox2.Text = "Set Hidden";
                    labelComboBox2_A.Text = "File name:";
                    labelComboBox2_B.Text = "True/False:";
                    break;
                case 3:
                    buttonComboBox2.Text = "Rename";
                    labelComboBox2_A.Text = "File name:";
                    labelComboBox2_B.Text = "Rename to:";
                    break;
                case 4:
                    buttonComboBox2.Text = "Copy";
                    labelComboBox2_A.Text = "File name1:";
                    labelComboBox2_B.Text = "File name2:";
                    break;
                case 5:
                    buttonComboBox2.Text = "Compare";
                    labelComboBox2_A.Text = "File name1:";
                    labelComboBox2_B.Text = "File name2:";
                    break;
                default:
                    buttonComboBox2.Enabled = false;
                    break;
            }
        }

        private void buttonComboBox1_Click(object sender, EventArgs e)
        {
            String option = buttonComboBox1.Text;
            try
            {
                if (textBoxOption1.Text == String.Empty)
                    throw new Exception("The input mustn't be empty!");
                listView1.Items.Clear();
                switch (option)
                {
                    case "Remove":
                        if (MyTinyFS.remove(textBoxOption1.Text))
                            listView1.Items.Add(String.Format("File \"{0}\" removed successfully!", textBoxOption1.Text));
                        break;
                    case "Encrypt":
                        if(MyTinyFS.encrypt(textBoxOption1.Text))
                            listView1.Items.Add(String.Format("All files were encrypted successfully!", textBoxOption1.Text));
                        break;
                    case "Decrypt":
                        if (MyTinyFS.decrypt(textBoxOption1.Text))
                            listView1.Items.Add(String.Format("All files were decrypted successfully!", textBoxOption1.Text));
                        break;
                    case "Compress":
                        if(MyTinyFS.compressFile(textBoxOption1.Text))
                            listView1.Items.Add(String.Format("File \"{0}\" compressed successfully!", textBoxOption1.Text));
                        break;
                    case "Uncompress":
                        if (MyTinyFS.uncompressFile(textBoxOption1.Text))
                            listView1.Items.Add(String.Format("File \"{0}\" uncompressed successfully!", textBoxOption1.Text));
                        break;
                }
                listView1.Columns[0].Width = -1;
                labelFSSize.Text = MyTinyFS.getSize().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

           
        }
        private void DisplayAllFiles()
        {
            listView1.Items.Clear();
            List<String> allFiles = MyTinyFS.listFiles();
            foreach (String file in allFiles)
            {
                listView1.Items.Add((String)file);
            }
            listView1.Columns[0].Width = -1;
        }
        private void displayAllFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayAllFiles();
        }

        private void byNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyTinyFS.sortByName();
            DisplayAllFiles();


        }

        private void byDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyTinyFS.sortByDate();
            DisplayAllFiles();
        }

        private void bySizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyTinyFS.sortBySize();
            DisplayAllFiles();
        }

        private void saveFileSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory());
            saveFile.Filter = "TinyMemFS|*.TFS";
            listView1.Items.Clear();
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (saveFile.FileName != null)
                    {
                        if (MyTinyFS.saveToDisk(saveFile.FileName))
                            listView1.Items.Add(String.Format("File System saved successfully to file {0}!", saveFile.FileName));
                        else
                            throw new Exception(String.Format("Failed saving File System "));
                        listView1.Columns[0].Width = -1;
                    }               

                    else
                        throw new Exception("File name can't be empty!");

                }
                catch (Exception exx)
                {
                    MessageBox.Show(exx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void loadFileSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream st;
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "TinyMemFS|*.TFS";
            loadFile.InitialDirectory = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory());
            listView1.Items.Clear();
            if (loadFile.ShowDialog() == DialogResult.OK)
            {
                if ((st = loadFile.OpenFile()) != null)
                {
                    try
                    {
                        string file = loadFile.FileName;
                        st.Flush();
                        st.Close();
                        if(MyTinyFS.loadFromDisk(file))
                            listView1.Items.Add(String.Format("File System loaded successfully! from {0}", file));
                        else
                            throw new Exception("Failed loading File System");
                        listView1.Columns[0].Width = -1;
                        labelFSSize.Text = MyTinyFS.getSize().ToString();

                    }
                    catch (Exception exx)
                    {
                        MessageBox.Show(exx.Message, "Wrong File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        st.Flush();
                        st.Close();
                    }
                }
            }
        }

        private void buttonComboBox2_Click(object sender, EventArgs e)
        {
            String option = buttonComboBox2.Text;
            try
            {
                if (textBoxOption2_A.Text == String.Empty || textBoxOption2_B.Text == String.Empty)
                    throw new Exception("The input mustn't be empty!");
                listView1.Items.Clear();
                switch (option)
                {
                    case "Add":
                        if (MyTinyFS.add(textBoxOption2_A.Text, textBoxOption2_B.Text))
                            listView1.Items.Add(String.Format("File \"{0}\" added successfully!", textBoxOption2_A.Text));
                        break;

                    case "Save":
                        if (textBoxOption2_A.Text == String.Empty || textBoxOption2_B.Text == String.Empty)
                            throw new Exception(String.Format("Input mustn't be empty!"));
                        if (MyTinyFS.save(textBoxOption2_A.Text, textBoxOption2_B.Text))
                            listView1.Items.Add(String.Format("File \"{0}\" saved to \"{1}\"!", textBoxOption2_A.Text, textBoxOption2_B.Text));
                        break;

                    case "Set Hidden":
                        if (!textBoxOption2_B.Text.Equals("True") && !textBoxOption2_B.Text.Equals("False") && 
                            !textBoxOption2_B.Text .Equals("1") && !textBoxOption2_B.Text.Equals("0"))
                            throw new Exception("Must Enter \"True\" or \"False\" or \"1\" or \"0\"");
                        if(textBoxOption2_B.Text.Equals("True") || textBoxOption2_B.Text.Equals("1"))
                        {
                            if(MyTinyFS.setHidden(textBoxOption2_A.Text, true)) 
                            {
                                listView1.Items.Add(String.Format("File \"{0}\" was hidden successfully!", textBoxOption2_A.Text));

                            }
                        }
                        else
                        {
                            if(MyTinyFS.setHidden(textBoxOption2_A.Text, false))
                                listView1.Items.Add(String.Format("File \"{0}\" was unhidden successfully!", textBoxOption2_A.Text));
                        }
                        break;
                    case "Rename":
                        if(MyTinyFS.rename(textBoxOption2_A.Text, textBoxOption2_B.Text))
                            listView1.Items.Add(String.Format("File \"{0}\" was renamed to \"{1}\"!", textBoxOption2_A.Text, textBoxOption2_B.Text));
                        break;
                    case "Copy":
                        if(MyTinyFS.copy(textBoxOption2_A.Text, textBoxOption2_B.Text))
                            listView1.Items.Add(String.Format("File \"{0}\" was copied to file \"{1}\"!", textBoxOption2_A.Text, textBoxOption2_B.Text));
                        break;
                    case "Compare":
                        if (MyTinyFS.compare(textBoxOption2_A.Text, textBoxOption2_B.Text))
                            listView1.Items.Add(String.Format("File \"{0}\" is equal to file \"{1}\"!", textBoxOption2_A.Text, textBoxOption2_B.Text));
                        else
                            listView1.Items.Add(String.Format("File \"{0}\" not equal to file \"{1}\"!", textBoxOption2_A.Text, textBoxOption2_B.Text));
                        break;
                }
                listView1.Columns[0].Width = -1;
                labelFSSize.Text = MyTinyFS.getSize().ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}