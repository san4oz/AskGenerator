﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AskGenerator.Mvc.Helpers {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class MailsData {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MailsData() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AskGenerator.Mvc.Helpers.MailsData", typeof(MailsData).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на &lt;h1&gt;&lt;span style=&quot;font-family: &apos;Lucida Grande CY&apos;, Arial, &apos;Liberation Sans&apos;, &apos;DejaVu Sans&apos;, sans-serif; font-size: 24px; font-weight: bold; line-height: 31px;&quot;&gt;Вас вітає &lt;em&gt;&lt;a href=&quot;[siteURL]&quot; title=&quot;NoM&quot;&gt;[siteName]&lt;/a&gt;&lt;/em&gt;&lt;/span&gt;&lt;/h1&gt; 
        ///
        ///&lt;table border=&quot;0&quot; cellpadding=&quot;0&quot; cellspacing=&quot;0&quot; style=&quot;font-family: &apos;Lucida Grande CY&apos;, Arial, &apos;Liberation Sans&apos;, &apos;DejaVu Sans&apos;, sans-serif; line-height: normal;&quot; width=&quot;100%&quot;&gt;
        ///  &lt;tbody&gt;
        ///    &lt;tr&gt;
        ///      &lt;td style=&quot;border-collapse: collapse;&quot;&gt;
        ///      &lt;table bgcolor=&quot;&quot; [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string ConirmRegistration_Body {
            get {
                return ResourceManager.GetString("ConirmRegistration_Body", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Вітаємо!
        ///
        ///Вашу електронну адресу було викоритсано для реєстрації на сайті [siteName].
        ///
        ///Щоб завершити реєстрацію перейдіть за наступним посиланням [confirmURL]
        ///
        ///Якщо ви не проходили реєстрацію, будь ласка, проігноруйте цей лист.
        ///
        ///Network of Mentors.
        /// </summary>
        internal static string ConirmRegistration_Plain {
            get {
                return ResourceManager.GetString("ConirmRegistration_Plain", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Завершити реєстрацію!.
        /// </summary>
        internal static string ConirmRegistration_Subj {
            get {
                return ResourceManager.GetString("ConirmRegistration_Subj", resourceCulture);
            }
        }
    }
}
