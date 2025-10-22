import http from 'k6/http';
import { check, sleep } from 'k6';

const BASE = __ENV.BASE_URL || 'http://localhost:5241';

export const options = {
  vus: 10,
  duration: '30s',
};

export default function () {
  const endpoints = [
    `${BASE}/Artistas`,
    `${BASE}/Artistas/${encodeURIComponent('Foo Fighters')}`,
    `${BASE}/Musicas`,
    `${BASE}/Musicas/${encodeURIComponent('Everlong')}`,
    `${BASE}/Generos`,
    `${BASE}/Generos/${encodeURIComponent('Rock')}`,
  ];

  for (const url of endpoints) {
    const res = http.get(url);
    // Accept 200 for found resources and 404 for specific-name lookups that might not exist
    check(res, {
      'status is 200 or 404': (r) => r.status === 200 || r.status === 404,
    });
    sleep(0.2);
  }
}