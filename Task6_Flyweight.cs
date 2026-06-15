using System;
using System.Collections.Generic;

class LightElementFlyweight
{
    public string TagName { get; }
    public DisplayType Display { get; }
    public ClosingType Closing { get; }
    public List<string> CssClasses { get; }

    public LightElementFlyweight(string tagName, DisplayType display, ClosingType closing, List<string> cssClasses)
    {
        TagName = tagName;
        Display = display;
        Closing = closing;
        CssClasses = cssClasses;
    }

    private string ClassAttr => CssClasses.Count > 0
        ? $" class=\"{string.Join(" ", CssClasses)}\""
        : "";

    public string GetOuterHTML(string innerContent)
    {
        if (Closing == ClosingType.Single)
            return $"<{TagName}{ClassAttr}/>";
        return $"<{TagName}{ClassAttr}>{innerContent}</{TagName}>";
    }
}

class FlyweightFactory
{
    private Dictionary<string, LightElementFlyweight> _cache = new Dictionary<string, LightElementFlyweight>();

    public LightElementFlyweight GetFlyweight(string tagName, DisplayType display, ClosingType closing, List<string> cssClasses)
    {
        string key = $"{tagName}_{display}_{closing}_{string.Join(",", cssClasses)}";
        if (!_cache.ContainsKey(key))
            _cache[key] = new LightElementFlyweight(tagName, display, closing, cssClasses);
        return _cache[key];
    }

    public int CacheCount => _cache.Count;
}

class FlyweightNode
{
    private LightElementFlyweight _flyweight;
    private string _innerContent;

    public FlyweightNode(LightElementFlyweight flyweight, string innerContent)
    {
        _flyweight = flyweight;
        _innerContent = innerContent;
    }

    public string OuterHTML => _flyweight.GetOuterHTML(_innerContent);
}
