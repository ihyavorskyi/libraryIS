﻿using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace New_Lib
{
    public class AddToDataBase
    {
        public static MySqlConnection conn = new MySqlConnection(LibraryForm.GetConnection());

        public static string add(int idAdd, DataGridView dataGridViewCatalog, string tableName, string Query)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                conn.Open();
                switch (idAdd)
                {
                    case 1:
                        genreAdd(dataGridViewCatalog);
                        tableName = ShowCatalog.ShowGenres(dataGridViewCatalog);
                        break;

                    case 2:
                        typeAdd(dataGridViewCatalog);
                        tableName = ShowCatalog.ShowTypes(dataGridViewCatalog);
                        break;

                    case 3:
                        authorAdd(dataGridViewCatalog);
                        tableName = ShowCatalog.ShowAuthor(dataGridViewCatalog);
                        break;

                    case 4:
                        pubHouseAdd(dataGridViewCatalog);
                        tableName = ShowCatalog.ShowPubHouses(dataGridViewCatalog);
                        break;

                    case 5:
                        bookAdd(dataGridViewCatalog);
                        tableName = ShowCatalog.ShowBookCatalog(dataGridViewCatalog, Query + "order by book.Code_book");
                        break;

                    case 6:
                        patentAdd(dataGridViewCatalog);
                        tableName = ShowCatalog.ShowPatents(dataGridViewCatalog);
                        break;

                    case 7:
                        articleAdd(dataGridViewCatalog);
                        tableName = ShowCatalog.ShowArticles(dataGridViewCatalog);
                        break;
                }
                MessageBox.Show("Added successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return tableName;
        }

        private static void articleAdd(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                string name = dataGridView.Rows[i].Cells[1].Value.ToString();
                string title = dataGridView.Rows[i].Cells[2].Value.ToString();
                string magazine = dataGridView.Rows[i].Cells[3].Value.ToString();
                string dateOfPublication = dataGridView.Rows[i].Cells[4].Value.ToString();
                string query = "insert into articles values (null,'" + name + "','" + title + "','" + magazine + "','" + dateOfPublication + "');";
                NewQuery.executeNonQuery(query, conn);
            }
            conn.Close();
        }

        private static void patentAdd(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                string book = dataGridView.Rows[i].Cells[1].Value.ToString();
                string number = dataGridView.Rows[i].Cells[2].Value.ToString();
                string country = dataGridView.Rows[i].Cells[3].Value.ToString();
                string dateOfApplication = dataGridView.Rows[i].Cells[4].Value.ToString();
                string dateOfPublication = dataGridView.Rows[i].Cells[4].Value.ToString();
                string query = "insert into patent values (null,'" + book + "','" + number + "','" + country + "','" + dateOfApplication + "','" + dateOfPublication + "');";
                NewQuery.executeNonQuery(query, conn);
            }
            conn.Close();
        }

        private static void genreAdd(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                string query = "insert into genre values (null,'" + dataGridView.Rows[i].Cells[1].Value.ToString() + "');";
                NewQuery.executeNonQuery(query, conn);
            }
            conn.Close();
        }

        private static void typeAdd(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                string query = "insert into type values (null,'" + dataGridView.Rows[i].Cells[1].Value.ToString() + "');";
                NewQuery.executeNonQuery(query, conn);
            }
            conn.Close();
        }

        private static void authorAdd(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                string name = dataGridView.Rows[i].Cells[1].Value.ToString();
                string surname = dataGridView.Rows[i].Cells[2].Value.ToString();
                string birthday = dataGridView.Rows[i].Cells[3].Value.ToString();
                string homeland = dataGridView.Rows[i].Cells[4].Value.ToString();
                string query = "insert into author values (null,'" + name + "','" + surname + "','" + birthday + "','" + homeland + "');";
                NewQuery.executeNonQuery(query, conn);
            }
            conn.Close();
        }

        private static void pubHouseAdd(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                string name = dataGridView.Rows[i].Cells[1].Value.ToString();
                string adress = dataGridView.Rows[i].Cells[2].Value.ToString();
                string city = dataGridView.Rows[i].Cells[3].Value.ToString();
                string mail = dataGridView.Rows[i].Cells[4].Value.ToString();
                string query = "insert into publishing_house values (null,'" + name + "','" + adress + "','" + city + "','" + mail + "');";
                NewQuery.executeNonQuery(query, conn);
            }
            conn.Close();
        }

        private static void bookAdd(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                string code_book = "";
                string title = dataGridView.Rows[i].Cells[1].Value.ToString();
                string author = dataGridView.Rows[i].Cells[2].Value.ToString();
                string pages = dataGridView.Rows[i].Cells[3].Value.ToString();
                string year = dataGridView.Rows[i].Cells[4].Value.ToString();
                string genre = dataGridView.Rows[i].Cells[5].Value.ToString();
                string type = dataGridView.Rows[i].Cells[6].Value.ToString();
                string pubHouse = dataGridView.Rows[i].Cells[7].Value.ToString();
                string count = dataGridView.Rows[i].Cells[8].Value.ToString();

                string query = "insert into book values (null,'" + title + "','" + pages + "','" + year + "','" + pubHouse + "','" + genre + "','" + type + "','" + count + "');";
                NewQuery.executeNonQuery(query, conn);

                query = "select max(Code_book) from book";
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    code_book = reader[0].ToString();
                }
                reader.Close();

                query = "insert into author_list values (null,'" + author + "','" + code_book + "');";
                NewQuery.executeNonQuery(query, conn);
            }
            conn.Close();
        }
    }
}