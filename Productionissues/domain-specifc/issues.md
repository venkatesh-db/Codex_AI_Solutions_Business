# Domain-specific: 50 business-logic issue scenarios

These are **diagnostic scenarios** derived from ONC healthcare safety guidance and OWASP business-logic testing guidance. The sources establish real failure classes; they do not prove that every exact scenario occurred in this repository. The healthcare examples are relevant to the patient-identifier symptoms supplied for this task, while the other examples show the same defect classes in commerce and finance.

**Documented healthcare evidence:** [AHRQ's summary of health IT related wrong-patient errors](https://psnet.ahrq.gov/issue/health-information-technology-related-wrong-patient-errors-context-critical) reports that some events reached patients, commonly through wrong medication administration or wrong tests or procedures. The rows below are investigation prompts based on documented classes, not claims of 50 distinct patient-safety incidents.

| # | Observable symptom or failed invariant | Evidence for failure class |
| ---: | --- | --- |
| 1 | Order is attached to a different patient record | [Patient identity](https://healthit.gov/clinical-quality-and-safety/safer-guides) |
| 2 | Two patients' records appear merged after registration | [Patient identity](https://healthit.gov/clinical-quality-and-safety/safer-guides) |
| 3 | Search returns the wrong patient with a similar name | [Patient identity](https://healthit.gov/clinical-quality-and-safety/safer-guides) |
| 4 | Result is filed under the wrong chart | [Patient identity](https://healthit.gov/clinical-quality-and-safety/safer-guides) |
| 5 | Patient identifier changes across a care handoff | [Patient identity](https://healthit.gov/clinical-quality-and-safety/safer-guides) |
| 6 | Prescribed dose differs from the saved order | [Medication order entry](https://healthit.gov/clinical-quality-and-safety/safer-guides/using-health-it/) |
| 7 | Medication pick list selects a similar drug name | [Medication order entry](https://healthit.gov/clinical-quality-and-safety/safer-guides/using-health-it/) |
| 8 | Allergy warning is absent for a known allergy | [Medication order entry](https://healthit.gov/clinical-quality-and-safety/safer-guides/using-health-it/) |
| 9 | Order frequency displayed to staff differs from the clinician's intent | [Medication order entry](https://healthit.gov/clinical-quality-and-safety/safer-guides/using-health-it/) |
| 10 | A discontinued order still appears active downstream | [Medication order entry](https://healthit.gov/clinical-quality-and-safety/safer-guides/using-health-it/) |
| 11 | Abnormal result reaches the chart but no follow-up queue | [Test-result follow-up](https://healthit.gov/clinical-quality-and-safety/safer-guides/using-health-it/) |
| 12 | Result notification is marked complete before review | [Test-result follow-up](https://healthit.gov/clinical-quality-and-safety/safer-guides/using-health-it/) |
| 13 | Referring clinician cannot see a returned result | [Test-result follow-up](https://healthit.gov/clinical-quality-and-safety/safer-guides/using-health-it/) |
| 14 | A corrected result does not supersede the prior value | [Test-result follow-up](https://healthit.gov/clinical-quality-and-safety/safer-guides/using-health-it/) |
| 15 | Patient is not notified after an abnormal result is acknowledged | [Test-result follow-up](https://healthit.gov/clinical-quality-and-safety/safer-guides/using-health-it/) |
| 16 | Invalid identifier passes syntax checks and enters the workflow | [Cross-industry data validation](https://owasp.org/www-project-web-security-testing-guide/stable/4-Web_Application_Security_Testing/10-Business_Logic_Testing/01-Test_Business_Logic_Data_Validation.html) |
| 17 | Negative quantity is accepted for a purchase or dosage field | [Cross-industry data validation](https://owasp.org/www-project-web-security-testing-guide/stable/4-Web_Application_Security_Testing/10-Business_Logic_Testing/01-Test_Business_Logic_Data_Validation.html) |
| 18 | A unit mismatch produces a plausible but wrong total | [Cross-industry data validation](https://owasp.org/www-project-web-security-testing-guide/stable/4-Web_Application_Security_Testing/10-Business_Logic_Testing/01-Test_Business_Logic_Data_Validation.html) |
| 19 | A future or impossible date is stored as valid | [Cross-industry data validation](https://owasp.org/www-project-web-security-testing-guide/stable/4-Web_Application_Security_Testing/10-Business_Logic_Testing/01-Test_Business_Logic_Data_Validation.html) |
| 20 | A logically invalid value passes between two services | [Cross-industry data validation](https://owasp.org/www-project-web-security-testing-guide/stable/4-Web_Application_Security_Testing/10-Business_Logic_Testing/01-Test_Business_Logic_Data_Validation.html) |
| 21 | Fulfillment starts before payment or approval | [Workflow order](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/06-Circumvention_of_Work_Flows/) |
| 22 | A later-stage API succeeds without prerequisite steps | [Workflow order](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/06-Circumvention_of_Work_Flows/) |
| 23 | Cancelled transaction leaves reward points or credits | [Workflow order](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/06-Circumvention_of_Work_Flows/) |
| 24 | Replayed state token advances a completed workflow | [Workflow order](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/06-Circumvention_of_Work_Flows/) |
| 25 | Parallel requests bypass a sequential approval gate | [Workflow order](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/06-Circumvention_of_Work_Flows/) |
| 26 | Client-edited price is accepted as authoritative | [Request integrity](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/02-Ability_to_Forge_Requests/) |
| 27 | Changed account ID routes a transaction to another owner | [Request integrity](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/02-Ability_to_Forge_Requests/) |
| 28 | Server trusts a submitted discount amount | [Request integrity](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/02-Ability_to_Forge_Requests/) |
| 29 | A forged status field skips business review | [Request integrity](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/02-Ability_to_Forge_Requests/) |
| 30 | A reused request changes a previously approved object | [Request integrity](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/02-Ability_to_Forge_Requests/) |
| 31 | Closed order can still be edited | [State integrity](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/03-Integrity_Checks/) |
| 32 | A finalized record can be deleted without reversal | [State integrity](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/03-Integrity_Checks/) |
| 33 | One service sees approved while another sees pending | [State integrity](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/03-Integrity_Checks/) |
| 34 | Audit event is missing after a state change | [State integrity](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/03-Integrity_Checks/) |
| 35 | A later update overwrites a more recent decision | [State integrity](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/03-Integrity_Checks/) |
| 36 | Reservation remains locked after its payment window | [Process timing](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/04-Process_Timing/) |
| 37 | Expired price quote can still be accepted | [Process timing](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/04-Process_Timing/) |
| 38 | Approval arrives after cancellation and reopens work | [Process timing](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/04-Process_Timing/) |
| 39 | Timeout occurs after side effect but before response | [Process timing](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/04-Process_Timing/) |
| 40 | A late callback changes a terminal state | [Process timing](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/04-Process_Timing/) |
| 41 | One-time coupon is redeemed twice | [Function-use limits](https://wstg.owasp.org/v4.1/4-Web_Application_Security_Testing/10-Business_Logic_Testing/05-Test_Number_of_Times_a_Function_Can_Be_Used_Limits/) |
| 42 | Refund endpoint accepts a second refund | [Function-use limits](https://wstg.owasp.org/v4.1/4-Web_Application_Security_Testing/10-Business_Logic_Testing/05-Test_Number_of_Times_a_Function_Can_Be_Used_Limits/) |
| 43 | Single-use token completes more than one action | [Function-use limits](https://wstg.owasp.org/v4.1/4-Web_Application_Security_Testing/10-Business_Logic_Testing/05-Test_Number_of_Times_a_Function_Can_Be_Used_Limits/) |
| 44 | Retry creates a duplicate job or order | [Function-use limits](https://wstg.owasp.org/v4.1/4-Web_Application_Security_Testing/10-Business_Logic_Testing/05-Test_Number_of_Times_a_Function_Can_Be_Used_Limits/) |
| 45 | Parallel submissions exceed an allowed quota | [Function-use limits](https://wstg.owasp.org/v4.1/4-Web_Application_Security_Testing/10-Business_Logic_Testing/05-Test_Number_of_Times_a_Function_Can_Be_Used_Limits/) |
| 46 | Order total differs from charged total | [Payment and sensitive-data flow](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/10-Payment_Functionality/) |
| 47 | Discount remains after eligible items are removed | [Payment and sensitive-data flow](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/10-Payment_Functionality/) |
| 48 | Shipment begins after payment authorization fails | [Payment and sensitive-data flow](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/10-Payment_Functionality/) |
| 49 | Retry creates two charges for one order | [Payment and sensitive-data flow](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/10-Payment_Functionality/) |
| 50 | A payment failure leaves inventory permanently reserved | [Payment and sensitive-data flow](https://wstg.owasp.org/latest/4-Web_Application_Security_Testing/10-Business_Logic/10-Payment_Functionality/) |
