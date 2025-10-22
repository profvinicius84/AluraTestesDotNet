import http from 'k6/http';
import { check, sleep } from 'k6';

const BASE = __ENV.BASE_URL || 'http://localhost:5241';

// This script ramps up users to find the stress point. Adjust maxVUs if your machine can go higher.
export const options = {
  stages: [
    { duration: '30s', target: 10 },
    { duration: '30s', target: 50 },
    { duration: '30s', target: 100 },
    { duration: '30s', target: 200 },
    { duration: '30s', target: 300 },
    // Hold max for a short time
    { duration: '30s', target: 500 },
    // Ramp down
    { duration: '30s', target: 0 },
  ],
  thresholds: {
    http_req_failed: ['rate<0.05'], // less than 5% failed
    http_req_duration: ['p(95)<2000'], // 95% requests should be under 2s
  },
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

  // Each VU requests all endpoints sequentially
  for (const url of endpoints) {
    const res = http.get(url);
    check(res, {
      'status is 200 or 404': (r) => r.status === 200 || r.status === 404,
    });
    // small think time to better simulate real users and avoid pure bursts
    sleep(0.1);
  }
}