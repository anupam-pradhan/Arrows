# ArrowNook: Logic Puzzle — Ads & Release Checklist

## Before enabling production ads and submitting to Google Play

This document lists **every step** required before the release build guard
(`ArrowNookReleaseGuard.cs`) will permit a non-development Android build.

---

## 1. AdMob account setup

- [ ] Sign into [AdMob](https://admob.google.com/) with your Google account.
- [ ] Create an app entry for **ArrowNook: Logic Puzzle** (Android).
- [ ] Copy the real **Android App ID** (format `ca-app-pub-XXXXXXXXXXXXXXXX~NNNNNNNNNN`).

## 2. Replace test IDs in Unity

| File | Field | Replace with |
|---|---|---|
| `Assets/GoogleMobileAds/Resources/GoogleMobileAdsSettings.asset` | `adMobAndroidAppId` | Your real Android App ID |
| `Assets/_Game/Scripts/Ads/ResultsBannerController.cs` | `BannerUnit` (Android) | Your real banner ad unit ID |
| `Assets/_Game/Scripts/Ads/InterstitialAdController.cs` | `InterstitialUnit` (Android) | Your real interstitial ad unit ID |

> **Never commit real Ad IDs to a public repo.**  
> Store them as a local `Assets/StreamingAssets/AdIds.json` file and load at runtime,
> or use Unity Cloud Build secrets / GitHub Actions secrets.

## 3. Privacy policy

- [ ] Write a privacy policy covering:
  - Data collected by Google Mobile Ads (device ID, IP, ad interactions).
  - Data stored locally (level progress, settings).
  - Contact information (your name and email).
- [ ] Host it at a permanent public URL (e.g. GitHub Pages, Notion, or Google Sites).
- [ ] Add the URL to the Play Console "App content" → "Privacy policy" field.
- [ ] Update `Assets/_Game/Resources/Branding/PrivacyNotice.txt` with the public URL.

## 4. Age / audience declaration (Play Console)

- [ ] In Play Console → **App content** → **Target audience and content**, set:
  - Target age: **13 and over**.
  - Confirm the game does not target children under 13.
- [ ] Complete the **Ads declaration**:
  - Contains ads: **Yes**.
  - Ad type: Banner + interstitial.
  - Ad SDK: Google AdMob.
- [ ] Complete the **Data safety** section for all SDKs (AdMob collects data).

## 5. Consent / UMP configuration

The consent flow is already wired (`GatherConsent` in `ResultsBannerController`).
Before release:
- [ ] In the AdMob console → **Privacy & messaging**, create a GDPR message and link it.
- [ ] Set `TagForUnderAgeOfConsent = False` in `FinishConsent()` once you have verified the
  audience is 13+ and your legal review is complete.

## 6. Remove the development build guard

Once all steps above are done, delete or update `ArrowNookReleaseGuard.cs`:

```csharp
// Remove the BuildFailedException line, or delete the file entirely.
```

## 7. Final build checklist

- [ ] Increment `PlayerSettings.bundleVersionCode`.
- [ ] Set **Build type = Release** (disable Development Build toggle).
- [ ] Run on a physical Android device and verify:
  - A real banner appears on the results screen after level 4.
  - A real interstitial appears after completing 3 levels (warmup timer applies).
  - Privacy-choices button appears in Settings for EEA users.
  - "Made with Unity" logo displays in Settings > About.
  - No original SERAP-KEREM branding is visible to end users.

## 8. Play Store submission

- [ ] Upload signed AAB to an Internal testing track first.
- [ ] Verify rating: set **ESRB E** (Everyone) / **PEGI 3** (no violence, no adult content).
- [ ] Fill in store listing: use the name **ArrowNook: Logic Puzzle**.
- [ ] Add screenshots (portrait, minimum 2 phones + 1 tablet).
- [ ] Promote to Production once internal testing passes.

---

_Developer: Anupam Pradhan, India — solo developer_
