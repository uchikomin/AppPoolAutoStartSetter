using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPoolAutoStartSetter
{
    public partial class FormMain : Form
    {
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
                    // アプリケーションプールを取得
                    var appPools = manager.ApplicationPools;

                    // 各アプリケーションプールに対して繰り返し
                    foreach (var pool in appPools)
                    {
                        // アプリケーションプール名を取得
                        var name = pool.Name;
                        // autoStart を取得
                        var autoStart = pool.AutoStart;

                        // ListViewItem を生成
                        var item = new ListViewItem();

                        // テキストを設定
                        item.Text = name;
                        item.SubItems.Add(autoStart.ToString());

                        // ListView に追加
                        listViewAppPool.Items.Add(item);
                    }
                }

                // ソート条件の初期化
                sorter.SortColumn = 0;
                sorter.Order = SortOrder.Ascending;

                // ソート
                listViewAppPool.Sort();
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
                    // アプリケーションプールを取得
                    var appPools = manager.ApplicationPools;

                    // ListView の項目に対して繰り返し
                    foreach (ListViewItem item in listViewAppPool.Items)
                    {
                        // アプリケーションプール名を取得
                        var name = item.Text;
                        // autoStart を取得
                        var autoStart = bool.Parse(item.SubItems[1].Text);

                        // アプリケーションプールに対して繰り返し
                        foreach (var pool in appPools)
                        {
                            // 要素名が異なっている場合
                            if (pool.Name != name)
                            {
                                // スキップ
                                continue;
                            }

                            // autoStart を false にする
                            pool.AutoStart = false;
                            // ループを抜ける
                            break;
                        }
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

        /// <summary>
        /// 実行中一覧出力ボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOutputRunning_Click(object sender, EventArgs e)
        {
            // 
            var dialog = new OpenFileDialog();
            // 設定
            dialog.CheckFileExists = false;
            dialog.AddExtension = true;
            dialog.DefaultExt = ".txt";
            dialog.Multiselect = false;

            // ダイアログを表示し、キャンセルされた場合
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                // 処理を終了
                return;
            }

            // ファイルパス
            var filePath = dialog.FileName;

            // ファイルが存在する場合
            if (File.Exists(filePath))
            {
                // OK以外の場合
                if (MessageBox.Show("すでにファイルが存在します。\nよろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }

            try
            {
                // プリケーションプール名のリスト
                var list = new List<string>();

                // ServerManager を取得
                using (var manager = new ServerManager())
                {
                    // アプリケーションプールを取得
                    var appPools = manager.ApplicationPools;

                    // アプリケーションプールに対して繰り返し
                    foreach (var pool in appPools)
                    {
                        // 要素名が異なっている場合
                        if (pool.State != ObjectState.Started)
                        {
                            // スキップ
                            continue;
                        }

                        // リストにアプリケーションプール名を追加
                        list.Add(pool.Name);
                    }
                }

                // ソート
                list.Sort();

                // ストリームを開く
                using (var sw = new StreamWriter(filePath))
                {
                    // アプリケーションプール名のリストに対して繰り返し
                    foreach (var name in list)
                    {
                        // アプリケーションプール名を出力
                        sw.WriteLine(name);
                    }
                }

                MessageBox.Show("実行中のアプリケーションプール名を出力しました");
            }
            catch (Exception ex)
            {
                MessageBox.Show("アプリケーションプール名の出力に失敗しました: " + ex.Message);
            }
        }
    }
}
