using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);//key verip , belli bri tipteki cache değerini okuyacağım
        object Get(string key);

        void Add(string key, object data, int duration);//duration : cache bekleme süresi

        bool IsAdd(string key);//cache den mi getireyim yoksa direk veritabanından getirip cache mi ekleyim?

        void Remove(string key);//belli bir key deki cache in ortadan kaldırılımasını sağlar

        void RemoveByPattern(string pattern); //özel bir patterna sahip olan cacheleri sil diyebilirm.Mesela GET ile başlayanları sil diyebilirm . o yüzden isimlendirme yapıları önem arz ediyor 


        //temel interface hazır . şimdi bunu implemente edelim => core tarafında CCC klasör altında
    }
}
