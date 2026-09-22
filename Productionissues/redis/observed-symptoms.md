# Redis production issues: observed symptoms

These are documented incidents in systems that depended on Redis. The symptoms below are what operators or users observed; the cause is included only to connect each symptom to the published incident. They are not claims that Redis itself had a software defect.

## 1. Redis unavailable during application startup — GitHub, January 2016

**Observed symptoms**

- GitHub served HTTP 503 error pages while much of its frontend fleet and load balancing equipment was still running.
- Monitoring showed unreachable servers, a spike in Redis connectivity exceptions, and increased connection attempts.
- Application processes failed to start, so backend capacity did not recover until Redis clusters were restored.

**Confirmed cause:** A data-center power disruption rebooted servers across several Redis clusters. An application startup path had a hard dependency on Redis availability. This is a documented application resilience defect triggered by infrastructure failure, not a Redis server bug.

**Source:** [GitHub, “January 28th Incident Report”](https://github.blog/news-insights/company-news/january-28th-incident-report/).

## 2. Redis authentication configuration broke clients — GitHub, January 2023

**Observed symptoms**

- Container registry requests began returning more HTTP 500 errors.
- Most GitHub Pages builds and GitHub Packages requests failed during the incident.
- Registry clients could not establish successful Redis connections after authentication was enabled.

**Confirmed cause:** The production deployment used a hard-coded Redis connection string that did not authenticate under the new configuration. GitHub reverted the configuration change to restore service.

**Source:** [GitHub, “Availability Report: February 2023,” January 30 incident](https://github.blog/news-insights/company-news/github-availability-report-february-2023/).

## 3. Redis load balancer sent traffic to the wrong host — GitHub, March 2026

**Observed symptoms**

- GitHub Actions runs queued: 95% did not start within five minutes, with an average delay of 30 minutes.
- Ten percent of runs failed with an infrastructure error.
- Jobs began running again after the load balancer configuration was corrected, while the accumulated queue took longer to drain.

**Confirmed cause:** A Redis infrastructure update introduced incorrect load balancer configuration and routed internal traffic to the wrong host.

**Source:** [GitHub, “Availability report: March 2026,” March 5 incident](https://github.blog/news-insights/company-news/github-availability-report-march-2026/).

## 4. Redis cluster degradation and connection saturation — GitHub, July 2026

**Observed symptoms**

- GitHub Actions workflow runs were delayed, exhausted retries, or failed with infrastructure errors.
- In the first period, about 7% of runs were delayed more than five minutes at peak, and 25% failed during the incident.
- In the second period, multiple Redis nodes failed; healthy nodes reached connection limits. At peak, 30% of runs were delayed more than five minutes, and 60% failed during the incident.

**Confirmed cause:** Overlapping regional configuration and capacity changes sent traffic to a degraded region. Returning traffic to a region still scaling led to Redis node failures and connection pressure.

**Source:** [GitHub, “Availability report: July 2026,” July 25 incident](https://github.blog/news-insights/company-news/github-availability-report-july-2026/).
