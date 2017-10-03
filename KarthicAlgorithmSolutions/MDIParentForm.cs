using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
    public partial class MDIParentForm : Form
    {
        private int childFormNumber = 0;

        public MDIParentForm()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void stringAndArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void page1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            Form1 child = new Form1();
            child.Location = new System.Drawing.Point(50, 50);
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Array and String " + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();
        }

        private void arrayPage3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            ArrayAndStringP3 child = new ArrayAndStringP3();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Array and String " + "Page 3";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void arrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            ArrayAndStringsP4 child = new ArrayAndStringsP4();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Array and String " + "Page 4";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void arrayPage2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

                 // Create a new form to represent the child form.
            Page2 child = new Page2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Array and String " + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void arrayPage5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            ArrayAndStringP5 child = new ArrayAndStringP5();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Array and String " + "Page 5";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void arrayPage6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            ArrayAndString child = new ArrayAndString();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Array and String " + "Page 6";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();


        }

        private void linkedListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void objectOrientedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void recursionDynamicProgrammingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MDIParentForm_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void specialTressToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void microsoftInterviewQuestionPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            MicrosoftIQ child = new MicrosoftIQ();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Microsoft Interview Questions " + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void binaryTreePage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
             if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
             BinaryTrees child = new BinaryTrees();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Binary Trees " + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();
        }

        private void binaryTreePage2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            BinaryTees child = new BinaryTees();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Binary Trees " + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void binaryTreePage3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            BinaryTreesPage3 child = new BinaryTreesPage3();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Binary Trees " + "Page 3";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void heapPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            Heap child = new Heap();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Heap" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void binaryManipulationPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
           BitManipulation child = new BitManipulation();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Bit Manipulation" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();



        }

        private void binaryManipulationPage2ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            BitManipulation1 child = new BitManipulation1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Bit Manipulation" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();


        }

        private void gayleLakkmannProblemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void moderateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

                
        


        }

        private void heapPage2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            HeapPage2 child = new HeapPage2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Heap" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void linkedListPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            LinkedListPage1 child = new LinkedListPage1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Linked List" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void linkedListPage2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            LinkedListPage2 child = new LinkedListPage2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Linked List" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void linkedListPage3ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            LinkedListPage3 child = new LinkedListPage3();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Linked List" + "Page 3";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void logicalProblemsPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            LogicalProblems child = new LogicalProblems();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Logical Problems" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void oOPSDesignPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            ObjectOrientedDesign child = new ObjectOrientedDesign();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Object Oriented Design" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void recursionAndDynamicProgramPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
          if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            RecursionAndDynamicProgrammingP1 child = new RecursionAndDynamicProgrammingP1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Recursion and Dynamic Programming" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();
        }

        private void sortingAndSearchPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            SortingAndSearch child = new SortingAndSearch();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Sorting and Search" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void specialTreesPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            SpecailTreePage1 child = new SpecailTreePage1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Special Trees" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void specialTreesPage2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            SpecialTreePage2 child = new SpecialTreePage2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Special Tree 2" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void stacksAndQueuePage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            StacksAndQueuesPage1 child = new StacksAndQueuesPage1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Stacks and Queue" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void stacksAndQueuePage2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            StackAndQueuesPage2 child = new StackAndQueuesPage2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Stacks and Queue" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void stacksAndQueuePage3ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            StackAndQueue child = new StackAndQueue();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Stacks and Queue" + "Page 3";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void microsoftPage2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            MicrosoftIQ2 child = new MicrosoftIQ2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Microsoft Interview Questions" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void expediaPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.

            ProblGlassdoorPage1 child = new ProblGlassdoorPage1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Tech Companies Interview Questions" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void page1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            GLModerateProblems_Page1 child = new GLModerateProblems_Page1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Gayle Lakkmann Moderate Problems" + "Page 1";
            child.Text = formText;
            child.Dock = DockStyle.Top;
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void page2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            GLModerateProblems_Page2 child = new GLModerateProblems_Page2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Gayle Lakkmann Moderate Problems" + "Page 2";
            child.Text = formText;
           
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void page3ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            GLModerateProblems_Page3 child = new GLModerateProblems_Page3();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Gayle Lakkmann Moderate Problems" + "Page 3";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.

            child.Show();
            

        }

        private void page1ToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            GLHardProblems_Page1 child = new GLHardProblems_Page1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Gayle Lakkmann Hard Problems" + "Page 1";
            child.Text = formText;
            child.Dock = DockStyle.Fill;
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.

            child.Show();

        }

        private void page2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            GLHardProblems_Page2 child = new GLHardProblems_Page2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Gayle Lakkmann Hard Problems" + "Page 2";
            child.Text = formText;
            child.Dock = DockStyle.Fill;
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.

            child.Show();

        }

        private void interviewQuestionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void techCompaniePage2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            TechCompanies child = new TechCompanies();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Tech companies" + "Page 2";
            child.Text = formText;
            child.Dock = DockStyle.Fill;
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.

            child.Show();

        }

        private void techCompaniesPage3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            TechCompany child = new TechCompany();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Tech companies" + "Page 3";
            child.Text = formText;
            child.Dock = DockStyle.Fill;
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.

            child.Show();

        }

        private void techCompaniesPage4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            TechCompanyPage4 child = new TechCompanyPage4();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Tech companies" + "Page 4";
            child.Text = formText;
            child.Dock = DockStyle.Fill;
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.

            child.Show();

        }

        private void techCompaniesPage5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            TechCompanyPage5 child = new TechCompanyPage5();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Tech companies" + "Page 5";
            child.Text = formText;
            child.Dock = DockStyle.Fill;
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.

            child.Show();

        }

        private void candidateQuestion1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            CandidateQuesPage1 child = new CandidateQuesPage1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "CandidateQuesPage1" + "Page 1";
            child.Text = formText;
            child.Dock = DockStyle.Fill;
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.

            child.Show();

        }

        private void candidatesPostingQuestionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void candidateQuestion2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            CandidateQuestionsPage2 child = new CandidateQuestionsPage2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "CandidateQuesPage1" + "Page 1";
            child.Text = formText;
            child.Dock = DockStyle.Fill;
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.

            child.Show();


        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void amazonQuestionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void set1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            AmazonQuestion1Page1 child = new AmazonQuestion1Page1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Amazon Questions" + "Page 1";
            child.Text = formText;
            child.Dock = DockStyle.Fill;
            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.

            child.Show();

        }

        private void teToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if (ActiveMdiChild != null)
            //{
            //    ActiveMdiChild.Close();
            //}

            //// Create a new form to represent the child form.
            //TechCompanyPage6 child = new TechCompanyPage6();
            //// Increment the private child count.
            //childFormNumber++;
            //// Set the text of the child form using the count of child forms.
            //String formText = "TechCompany" + "Page 6";
            //child.Text = formText;
            //child.Dock = DockStyle.Fill;
            //// Make the new form a child form.
            //child.MdiParent = this;
            //// Display the child form.

            //child.Show();

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            MicrosoftIQ child = new MicrosoftIQ();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Microsoft Interview Questions" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            RelatedProblemsArrayPage1 child = new RelatedProblemsArrayPage1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "RelatedProblemsArray" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();
        }

        private void miToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            AmazonQuestion1Page1 child = new AmazonQuestion1Page1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "AmazonQuestion" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void page1ToolStripMenuItem3_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            MicrosoftOTSPage1 child = new MicrosoftOTSPage1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Microsoft OTS" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void page2ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            MicrosoftOTSPage2 child = new MicrosoftOTSPage2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Microsoft OTS" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void page3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            MicrosoftOTSPage3 child = new MicrosoftOTSPage3();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Microsoft OTS" + "Page 3";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void sortingAndSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dfhfhdfhdfToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void page1ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            ProblGlassdoorPage1 child = new ProblGlassdoorPage1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Glassdoor problems" + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void page2ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            GlassdoorPage2 child = new GlassdoorPage2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Glassdoor problems" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void oOPSDesignPage2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            OODPage2 child = new OODPage2();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Glassdoor problems" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void oOPSDesignPage3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            OODPage3 child = new OODPage3();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Glassdoor problems" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();


        }

        private void page1ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            OODPage3 child = new OODPage3();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Glassdoor problems" + "Page 2";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();


        }

        private void implementationProblemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            


        }

        private void page1ToolStripMenuItem6_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            ImplementationProblems child = new ImplementationProblems();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Implementation Problems " + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void page3ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            GlassdoorPage3 child = new GlassdoorPage3();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "Glassdoor Problems " + "Page 3";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();

        }

        private void microsoftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            // Create a new form to represent the child form.
            NGU2017QuestionsPage1 child = new NGU2017QuestionsPage1();
            // Increment the private child count.
            childFormNumber++;
            // Set the text of the child form using the count of child forms.
            String formText = "NGU 2017 Questions " + "Page 1";
            child.Text = formText;

            // Make the new form a child form.
            child.MdiParent = this;
            // Display the child form.
            child.Show();
        }
    }
}
