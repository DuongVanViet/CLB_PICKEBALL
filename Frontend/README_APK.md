# Building APKs for the Flutter Frontend

This file explains how to produce debug and release APKs for the Flutter app located in `Frontend/`.

Prerequisites
- Install Flutter and add `flutter` to your PATH. Verify with `flutter --version`.
- Java JDK (OpenJDK 11+ recommended) and Android SDK installed.

Quick commands (from repo root)

Debug APK (unsigned debug build, signed with debug key):

```powershell
cd Frontend
.
\scripts\build-debug-apk.ps1
```

Release APK (signed if `android/key.properties` exists, otherwise uses debug key):

```powershell
cd Frontend
\scripts\build-release-apk.ps1
```

Signing for release (recommended)

1. Create a release keystore (example):

```powershell
keytool -genkey -v -keystore android/app/my-release-key.jks -keyalg RSA -keysize 2048 -validity 10000 -alias pcm_key
```

2. Create `android/key.properties` (do NOT commit this file). Example contents:

```
storePassword=YOUR_STORE_PASSWORD
keyPassword=YOUR_KEY_PASSWORD
keyAlias=pcm_key
storeFile=app/my-release-key.jks
```

3. Build release APK (the Gradle setup in `android/app/build.gradle.kts` will pick up `key.properties` if present):

```powershell
cd Frontend
\scripts\build-release-apk.ps1
```

APK outputs
- Debug APK: `Frontend/build/app/outputs/flutter-apk/app-debug.apk`
- Release APK: `Frontend/build/app/outputs/flutter-apk/app-release.apk`

CI option
- If you want automated builds, I can add a GitHub Actions workflow to build and upload the APK as an artifact. Let me know if you want that.
