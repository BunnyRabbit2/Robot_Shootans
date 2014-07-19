using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RobotShootans.Engine
{
    class GameOptions
    {
        int _musicVolume = 10;
        int _sfxVolume = 10;

        /// <summary>The volume of the music</summary>
        public int MusicVolume
        { 
            get { return _musicVolume; }
            set { _musicVolume = MathHelper.Clamp(value, 0, 10); }
        }
        /// <summary>The volume of the sound effects</summary>
        public int SfxVolume
        {
            get { return _sfxVolume; }
            set { _sfxVolume = MathHelper.Clamp(value, 0, 10); }
        }

        bool _musicOn = true;
        bool _sfxOn = true;

        /// <summary>If the music is on or not</summary>
        public bool MusicOn { get { return _musicOn; } set { _musicOn = value; } }
        /// <summary>If the sound effects will play or not</summary>
        public bool SfxOn { get { return _sfxOn; } set { _sfxOn = value; } }

        int _windowWidth = 960;
        int _windowHeight = 600;

        /// <summary>The width of the window</summary>
        public int WindowWidth { get { return _windowWidth; } }
        /// <summary>The height of the window</summary>
        public int WindowHeight { get { return _windowHeight; } }

        bool _fullScreen = false;
        /// <summary>Returns if to display full screen or not</summary>
        public bool FullScreen { get { return _fullScreen; } }

        /// <summary>Loads the options file or sets default values if not found</summary>
        public void LoadOptions()
        {
#if DEBUG
            if ( File.Exists("../../../Docs/options.xml") )
            {
                XDocument doc = XDocument.Load("../../../Docs/options.xml");
#else
            if ( File.Exists("options.xml") )
            {
                XDocument doc = XDocument.Load("options.xml");
#endif

                var o = doc.Descendants("options");

                foreach (var thing in o)
                {
                    var i = thing.Element("music-vol").Value;

                    _musicVolume = int.Parse(thing.Element("music-vol").Value);
                    _sfxVolume = int.Parse(thing.Element("sfx-vol").Value);

                    _musicOn = bool.Parse(thing.Element("music-on").Value);
                    _sfxOn = bool.Parse(thing.Element("sfx-on").Value);

                    _windowWidth = int.Parse(thing.Element("window-width").Value);
                    _windowHeight = int.Parse(thing.Element("window-height").Value);

                    _fullScreen = bool.Parse(thing.Element("full-screen").Value);
                }
            }
            else
            {
                LogFile.LogStringLine("Failed to find options.xml. Options set to default.");
            }
        }

        /// <summary>Writes the options file out</summary>
        public void WriteOptions()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
#if DEBUG
            XmlWriter optionsOut = XmlWriter.Create("../../../Docs/options.xml", settings);
#else
            XmlWriter optionsOut = XmlWriter.Create("options.xml", settings);
#endif

            if (optionsOut != null)
            {
                optionsOut.WriteStartDocument();
                optionsOut.WriteStartElement("options");

                optionsOut.WriteElementString("music-vol", _musicVolume.ToString());
                optionsOut.WriteElementString("sfx-vol", _sfxVolume.ToString());

                optionsOut.WriteElementString("music-on", _musicOn.ToString());
                optionsOut.WriteElementString("sfx-on", _sfxOn.ToString());

                optionsOut.WriteElementString("window-width", _windowWidth.ToString());
                optionsOut.WriteElementString("window-height", _windowHeight.ToString());

                optionsOut.WriteElementString("full-screen", _fullScreen.ToString());

                optionsOut.WriteEndElement();
                optionsOut.WriteEndDocument();

                optionsOut.Close();
            }
        }
    }
}
