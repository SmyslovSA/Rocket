using System;
using System.Collections.Generic;
using AngleSharp.Parser.Html;
using Rocket.Parser.Core.AlbumInfo;
using Rocket.Parser.Models;

namespace Rocket.Parser.Core
{
    /// <summary>
    /// Управление парсером
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ParserWorker<T> where T : class
    {
        IParser<T> _parser;
        IParserSettings _parserSettings;

        HtmlLoader _loader;

        bool _isActive;

        #region Properties

        public IParser<T> Parser
        {
            get
            {
                return _parser;
            }
            set
            {
                _parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return _parserSettings;
            }
            set
            {
                _parserSettings = value;
                _loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        #endregion

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public ParserWorker(IParser<T> parser)
        {
            this._parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this._parserSettings = parserSettings;
        }

        public void Start()
        {
            _isActive = true;
            Worker();
        }

        public void Start(AlbumInfoReleaseSettings settings, ICollection<ResourceItem> resourceItems)
        {
            this.Settings = settings;
            _isActive = true;
            ReleaseWorker(resourceItems);
        }

        public void Abort()
        {
            _isActive = false;
        }

        private async void Worker()
        {
            for(int i = _parserSettings.StartPoint; i <= _parserSettings.EndPoint; i++)
            {
                if (!_isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await _loader.GetSourceById(i.ToString());
                var domParser = new HtmlParser();

                var document = await domParser.ParseAsync(source);

                var result = _parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            _isActive = false;
        }

        private async void ReleaseWorker(ICollection<ResourceItem> resourceItems)
        {
            foreach (var resourceItem in resourceItems)
            {
                if (!_isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await _loader.GetSourceById(resourceItem.ResourceItemLink);
                var domParser = new HtmlParser();

                var document = await domParser.ParseAsync(source);

                var result = _parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            _isActive = false;
        }


    }
}
