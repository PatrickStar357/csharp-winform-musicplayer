using NAudio.Wave;
using NAudio.Vorbis;
using System;
using System.IO;
using NVorbis;

namespace WindowsMusic1
{
    public partial class Form1 : Form
    {
        string[] files;
        List<string> localmusiclist = new List<string> { };
        private WaveOutEvent waveOut;
        private VorbisWaveReader vorbisFileReader;

        public Form1()
        {
            InitializeComponent();
        }

        private void musicplay(string filename)
        {
            string extension = Path.GetExtension(filename);
            if (extension == ".ogg")
            {
                if (vorbisFileReader != null)
                {
                    if (waveOut.PlaybackState != PlaybackState.Stopped)
                    {
                        waveOut.Stop();
                        vorbisFileReader.Dispose();
                    }
                }
                vorbisFileReader = new VorbisWaveReader(filename);
                waveOut = new WaveOutEvent();
                waveOut.Init(vorbisFileReader);
                waveOut.Play();
            }
            else
            {
                if (vorbisFileReader != null)
                {
                    if (waveOut.PlaybackState != PlaybackState.Stopped)
                    {
                        waveOut.Stop();
                        vorbisFileReader.Dispose();
                    }
                }
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void musicpause(string filename)
        {
            string extension = Path.GetExtension(filename);
            if (extension == ".ogg")
            {
                waveOut.Pause();
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Ñ¡ÔñÒôÆµ|*.mp3;*,wav;*,flac;*.ogg";
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                if (files != null)
                {
                    Array.Clear(files, 0, files.Length);
                }
                files = openFileDialog1.FileNames;

                string[] array = files;

                foreach (string x in array)
                {
                    listBox1.Items.Add(x);
                    localmusiclist.Add(x);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (localmusiclist.Count > 0)
            {
                axWindowsMediaPlayer1.URL = localmusiclist[listBox1.SelectedIndex];
                musicplay(axWindowsMediaPlayer1.URL);
                label1.Text = Path.GetFileNameWithoutExtension(axWindowsMediaPlayer1.URL);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            musicpause(axWindowsMediaPlayer1.URL);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
            label2.Text = trackBar1.Value + "%";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int nextIndex = listBox1.SelectedIndex + 1;
            if (nextIndex >= localmusiclist.Count)
            {
                nextIndex = 0;
            }
            axWindowsMediaPlayer1.URL = localmusiclist[nextIndex];
            musicplay(axWindowsMediaPlayer1.URL);
            label1.Text = Path.GetFileNameWithoutExtension(localmusiclist[nextIndex]);
            listBox1.SelectedIndex = nextIndex;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            musicplay(axWindowsMediaPlayer1.URL);
        }
    }
}
