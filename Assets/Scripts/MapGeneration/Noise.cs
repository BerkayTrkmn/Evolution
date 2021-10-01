using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight,int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        // dalgalar için offset()
        for (int i = 0; i < octaves; i++) {
            float offsetX = prng.Next(-100000, 100000) +offset.x;//
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        
        }
        //scale 0 dan küçük olursa program bozuluyor(unity)
        if (scale <= 0)
            scale = 0.0001f;
        //max - min dalga boyu belirleyicisi
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        //scale artınca sol yukarıya scale oluyor bunu ortaya almak için haritanın yarısında çıkarıyoruz
        float halfWidth = mapWidth / 2;
        float halfHeight = mapWidth / 2;


        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                
                for (int i = 0; i < octaves; i++) {
                    // kaç tane dalga boyu varsa ona göre perlin noise oluşturuluyor ve üstüne offset ekleniyor
                    float sampleX = (x-halfWidth) / scale * frequency+ octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency+ octaveOffsets[i].y;

                    //perlin noise değeri hesaplanıyor normalde 0 ile 1 arasında değeri vardır burada -1 ile 1 arasında
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                   
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;

                }
                if (noiseHeight > maxNoiseHeight) {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }
                //mapin içine atılıyor değerler
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {

                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }


        return noiseMap;
    }
}
