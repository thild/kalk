﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scriban;
using Scriban.Parsing;
using Scriban.Runtime;
using Scriban.Syntax;

namespace Kalk.Core
{
    public class KalkUnits : KalkObjectWithAlias, IScriptCustomFunction
    {
        public int RequiredParameterCount => 0;

        public int ParameterCount => 0;

        public ScriptVarParamKind VarParamKind => ScriptVarParamKind.None;

        public Type ReturnType => typeof(object);

        public KalkUnits(KalkEngine engine) : base(engine)
        {
        }
        
        public ScriptParameterInfo GetParameterInfo(int index)
        {
            throw new NotSupportedException("Units don't have any parameters.");
        }
        
        public bool TryGetValue(string key, out KalkUnit value)
        {
            value = null;
            if (TryGetValue(null, new SourceSpan(), key, out var valueObj))
            {
                value = (KalkUnit) valueObj;
                return true;
            }
            return false;
        }
        
        public override bool TrySetValue(TemplateContext context, SourceSpan span, string member, object value, bool readOnly)
        {
            // In the case of using KalkUnits outside of the scripting engine
            if (context == null) return base.TrySetValue(null, span, member, value, readOnly);

            // Otherwise, we are not allowing to modify this object.
            throw new ScriptRuntimeException(span, "Units object can't be modified directly. You need to use the command `unit` instead.");
        }

        public object Invoke(TemplateContext context, ScriptNode callerContext, ScriptArray arguments, ScriptBlockStatement blockStatement)
        {
            if (!(callerContext.Parent is ScriptExpressionStatement))
            {
                return this;
            }

            var engine = (KalkEngine) context;
            if (this.All(x => x.Value.GetType() != typeof(KalkUnit)))
            {
                engine.WriteHighlightLine($"# No Units defined (e.g try `import StandardUnitsModule`)");
            }
            else
            {
                Display((KalkEngine) context, "Builtin Units", symbol => symbol.GetType() == typeof(KalkUnit) && !symbol.IsUser);
                Display((KalkEngine) context, "User Defined Units", symbol => symbol.GetType() == typeof(KalkUnit) && symbol.IsUser);
            }

            return null;
        }

        public void Display(KalkEngine engine, string title, Func<KalkUnit, bool> filter, bool addBlankLine = true)
        {
            if (engine == null) throw new ArgumentNullException(nameof(engine));
            if (title == null) throw new ArgumentNullException(nameof(title));

            var alreadyPrinted = new HashSet<KalkUnit>();
            bool isFirst = true;
            foreach (var unitKey in this.Keys.OrderBy(x => x))
            {
                var unit = this[unitKey] as KalkUnit;
                if (unit == null || !alreadyPrinted.Add(unit) || unit.Parent != null || (filter != null && !filter(unit))) continue;

                if (isFirst)
                {
                    engine.WriteHighlightLine($"# {title}");
                }
                else if (addBlankLine)
                {
                    engine.WriteHighlightLine("");
                }
                isFirst = false;

                unit.Invoke(engine, engine.CurrentNode, null, null);
            }
        }
        
        public ValueTask<object> InvokeAsync(TemplateContext context, ScriptNode callerContext, ScriptArray arguments, ScriptBlockStatement blockStatement)
        {
            return new ValueTask<object>(Invoke(context, callerContext, arguments, blockStatement));
        }
    }
}