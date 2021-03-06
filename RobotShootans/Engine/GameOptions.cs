﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RobotShootans.Engine
{
    /// <summary>
    /// The game options
    /// </summary>
    public class GameOptions
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
        public bool MusicOn { get { return _musicOn; } }
        /// <summary>If the sound effects will play or not</summary>
        public bool SfxOn { get { return _sfxOn; } }

        int _windowWidth = 960;
        int _windowHeight = 600;

        /// <summary>The width of the window</summary>
        public int WindowWidth { get { return _windowWidth; } }
        /// <summary>The height of the window</summary>
        public int WindowHeight { get { return _windowHeight; } }

        bool _fullScreen = false;
        /// <summary>Returns if to display full screen or not</summary>
        public bool FullScreen { get { return _fullScreen; } }

        /// <summary>
        /// If the options have been changed
        /// </summary>
        public bool OptionsChanged = false;

        private List<Vector2> _supportedResolutions = new List<Vector2>();
        /// <summary>
        /// The resolutions supported by the current graphics device
        /// </summary>
        public List<Vector2> SupportedResolutions { get { return _supportedResolutions; } }

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
                setSupportedResolutions();

                var o = doc.Descendants("options");

                foreach (var thing in o)
                {
                    // Attempts to parse options in and if a parse fails it defaults the value

                    if (!int.TryParse(thing.Element("music-vol").Value, out _musicVolume))
                        _musicVolume = 10;
                    if (!int.TryParse(thing.Element("sfx-vol").Value, out _sfxVolume))
                        _sfxVolume = 10;

                    if (!bool.TryParse(thing.Element("music-on").Value, out _musicOn))
                        _musicOn = true;
                    if (!bool.TryParse(thing.Element("sfx-on").Value, out _sfxOn))
                        _sfxOn = true;

                    if (!int.TryParse(thing.Element("window-width").Value, out _windowWidth))
                        _windowWidth = 960;
                    if (!int.TryParse(thing.Element("window-height").Value, out _windowHeight))
                        _windowHeight = 600;

                    if (!bool.TryParse(thing.Element("full-screen").Value, out _fullScreen))
                        _fullScreen = false;
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

        private void setSupportedResolutions()
        {
            foreach (var dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                Vector2 newRes = new Vector2(dm.Width, dm.Height);

                if (!_supportedResolutions.Exists(r => r.X == newRes.X && r.Y == newRes.Y))
                    _supportedResolutions.Add(newRes);
            }
        }

        /// <summary>Switchs music off or on</summary>
        public void SwitchMusic() { _musicOn = !_musicOn; OptionsChanged = true; }
        /// <summary>Switch SFX off or on</summary>
        public void SwitchSFX() { _sfxOn = !_sfxOn; OptionsChanged = true; }
        /// <summary>Increase music volume</summary>
        public void increaseMusicVolume() { if (_musicVolume < 10) _musicVolume++; OptionsChanged = true; }
        /// <summary>Decrease music volume</summary>
        public void decreaseMusicVolume() { if (_musicVolume > 0) _musicVolume--; OptionsChanged = true; }
        /// <summary>Increase sound effects volume</summary>
        public void increaseSfxVolume() { if (_sfxVolume < 10) _sfxVolume++; OptionsChanged = true; }
        /// <summary>Decrease sound effects volume</summary>
        public void decreaseSfxVolume() { if (_sfxVolume > 0) _sfxVolume--; OptionsChanged = true; }
        /// <summary>Switch whether the game is displayed full screen or not</summary>
        public void switchFullScreen() { _fullScreen = !_fullScreen; OptionsChanged = true; }
        /// <summary>Sets the resolution</summary>
        /// <param name="heightIn">The new width to use</param>
        /// <param name="widthIn">The new height to use</param>
        public void changeResolution(int widthIn, int heightIn)
        {
            Vector2 res = _supportedResolutions.Find(r => r.X == widthIn && r.Y == heightIn);

            if(res != null)
            {
                _windowWidth = (int)res.X;
                _windowHeight = (int)res.Y;
                OptionsChanged = true;
            }
        }
    }
}
