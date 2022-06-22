using Microsoft.Office;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.References
{
    public partial class PurchaseOrderSpecimen : Form
    {
        string filename = "";
        string File_extension = "";
        private string ProfileID = "";
        private string LocID = "";
        private int ImageID = 0;

        public PurchaseOrderSpecimen(string pId, string locId )
        {
            ProfileID = pId;
            LocID = locId;
            InitializeComponent();
        }

        private void PurchaseOrderSpecimen_Load(object sender, EventArgs e)
        {
            LoadImages();
            btnSave.Enabled = false;
        }

        Image ConvertBinarytoImage(byte[] binary)
        {
            using (MemoryStream ms = new MemoryStream(binary))
            {
                return Image.FromStream(ms);
            }
        }

        private byte[] ConvertImageToBinary(System.Drawing.Image imageToConvert, string formatimg)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    if(formatimg == ".png")
                        imageToConvert.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    else if (formatimg == ".jpeg" || formatimg == ".jpg")
                        imageToConvert.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog openPic = new OpenFileDialog() {Filter = "Image |*.jpg ; *.png; *.jpeg", ValidateNames = true ,Multiselect = false})
            {
                if(openPic.ShowDialog() == DialogResult.OK)
                {
                    filename = openPic.FileName;
                    pictureBox1.Image = Image.FromFile(filename);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    File_extension = Path.GetExtension(filename);
                    btnSave.Enabled = true;
                }   
            }
          
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && txtDesc.Text != "")
            {
                SaveImage();
                LoadImages();
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Image and description is mandatory");
            }
        }

        void SaveImage()
        {
            Dictionary<string, object> paramimg = new Dictionary<string, object>();
            string sqlImg = "";
            if (ImageID == 0)
            {
                sqlImg = @"INSERT INTO Images(Specimen,
                                                LocationID,
                                                ProfileID,
                                                Description,
                                                ImageType) 
                                      VALUES (@Specimen,
                                              @LocationID,
                                              @ProfileID,
                                              @Description,
                                              @ImageType)";
            }
            else
            {
                sqlImg = @"UPDATE Images SET Specimen = @Specimen, 
                                             LocationID = @LocationID, 
                                             ProfileID = @ProfileID, 
                                             Description = @Description,
                                             ImageType = @ImageType
                                        WHERE ImageID = @ImageID";
                paramimg.Add("@ImageID", ImageID);
            }

            paramimg.Add("@Specimen", ConvertImageToBinary(pictureBox1.Image, File_extension.ToLower()));
            paramimg.Add("@LocationID", LocID);
            paramimg.Add("@ProfileID", ProfileID);
            paramimg.Add("@Description", txtDesc.Text);
            paramimg.Add("@ImageType", File_extension.ToLower());
            int imgid = CommonClass.runSql(sqlImg, CommonClass.RunSqlInsertMode.QUERY, paramimg);
            if (imgid > 0)
            {
                string titles = "Information";
                MessageBox.Show("Record has been saved.", titles);
            }
        }

        void LoadImages()
        {
            dgImg.Rows.Clear();
            string selectSql = "SELECT * From Images WHERE ProfileID = " + ProfileID + " AND LocationID = " + LocID;
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, selectSql);
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                DataRow dr = dt.Rows[x];
                dgImg.Rows.Add();
                dgImg.Rows[x].Cells[0].Value = dr["Description"].ToString();
                dgImg.Rows[x].Cells[1].Value = dr["Specimen"];
                dgImg.Rows[x].Cells["ID"].Value = dr["ImageID"];
                dgImg.Rows[x].Cells["ImageType"].Value = dr["ImageType"].ToString();
            }

            if (dgImg.Rows.Count > 0)
            {
                dgImg.Rows[0].Selected = true;
                dgImg_CellClick(null, null);
            }
        }

        private void dgImg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pictureBox1.Image = ConvertBinarytoImage((byte[])dgImg.SelectedRows[0].Cells[1].Value);
            txtDesc.Text = dgImg.SelectedRows[0].Cells[0].Value.ToString();
            ImageID = Convert.ToInt32(dgImg.SelectedRows[0].Cells["ID"].Value);
            File_extension = dgImg.SelectedRows[0].Cells["ImageType"].Value.ToString();
            btnSave.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
