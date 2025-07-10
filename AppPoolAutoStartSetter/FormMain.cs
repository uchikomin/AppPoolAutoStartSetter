using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPoolAutoStartSetter
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// セクション名：アプリケーションプール
        /// </summary>
        private static readonly string APP_POOL_SECTION_NAME = "system.applicationHost/applicationPools";

        /// <summary>
        /// 要素名：アプリケーションプール名
        /// </summary>
        private static readonly string ELEM_NAME = "name";

        /// <summary>
        /// 要素名：autoStart
        /// </summary>
        private static readonly string ELEM_AUTO_START = "autoStart";


        /// <summary>
        /// Column Sorter
        /// </summary>
        private ListViewColumnSorter sorter = new ListViewColumnSorter();

        public FormMain()
        {
            InitializeComponent();
        }

        // 参考
        // https://learn.microsoft.com/ja-jp/iis/configuration/system.applicationhost/applicationpools/applicationpooldefaults/

        private void LoadAppPool()
        {
            // 描画一時停止
            listViewAppPool.BeginUpdate();

            // ListViewの項目を削除
            listViewAppPool.Items.Clear();

            try
            {
                // ServerManager を取得
                using (var manager = new ServerManager())
                {
                    // アプリケーションホスト設定を取得
                    var config = manager.GetApplicationHostConfiguration();

                    // アプリケーションプール情報を取得
                    var appPoolSection = config.GetSection(APP_POOL_SECTION_NAME);

                    // 各要素について繰り返し
                    foreach (var elem in appPoolSection.ChildElements)
                    {
                        // アプリケーションプール名
                        var name = elem[ELEM_NAME] as string;
                        // autoStart の値
                        var autoStart = elem[ELEM_AUTO_START] as bool?;

                        // ListViewItem を生成
                        var item = new ListViewItem();

                        // テキストを設定
                        item.Text = name;
                        item.SubItems.Add(autoStart.HasValue ? autoStart.ToString() : true.ToString());

                        // ListView に追加
                        listViewAppPool.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("アプリケーションプールの読込に失敗しました: " + ex.Message);
            }

            // 描画再開
            listViewAppPool.EndUpdate();

            // すべて無効ボタンの有効化
            buttonDisableAll.Enabled = listViewAppPool.Items.Count > 0;
        }

        /// <summary>
        /// フォームを読み込んだときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Sorter を設定
            listViewAppPool.ListViewItemSorter = sorter;

            // すべて無効ボタンを無効化
            buttonDisableAll.Enabled = false;
        }

        /// <summary>
        /// 読込ボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            // アプリプールを読込
            LoadAppPool();
        }

        /// <summary>
        /// すべて無効を押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDisableAll_Click(object sender, EventArgs e)
        {
            try
            {
                // ServerManager を取得
                using (var manager = new ServerManager())
                {
                    // アプリケーションホスト設定を取得
                    var config = manager.GetApplicationHostConfiguration();

                    // アプリケーションプールのセクションを取得
                    var appPoolSection = config.GetSection(APP_POOL_SECTION_NAME);

                    // ListView の項目に対して繰り返し
                    foreach (ListViewItem item in listViewAppPool.Items)
                    {
                        // アプリケーションプール名を取得
                        var name = item.Text;
                        // autoStart を取得
                        var autoStart = bool.Parse(item.SubItems[0].Text);

                        // アプリケーションプールの要素を取得
                        var elem = appPoolSection.GetChildElement(name);

                        // 要素が取得できなかった場合
                        if (elem == null)
                        {
                            // スキップ
                            continue;
                        }

                        // autoStart を false にする
                        elem[ELEM_AUTO_START] = false;
                    }

                    // 変更を保存する
                    manager.CommitChanges();
                }

                // アプリケーションプールを読込
                LoadAppPool();
            }
            catch (Exception ex)
            {
                MessageBox.Show("アプリケーションプールの無効化に失敗しました: " + ex.Message);
            }
        }

        /// <summary>
        /// ListView の Column をクリックしたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAppPool_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == sorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            listViewAppPool.Sort();
        }
    }
}
