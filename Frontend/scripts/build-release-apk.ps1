Param()
Write-Host "Building Flutter release APK..."
Push-Location -Path "$(Resolve-Path ..)" | Out-Null
try {
    flutter pub get
    # Ensure key.properties exists in android/ for signing. If you don't want signing, Flutter will use debug key.
    flutter build apk --release
    Write-Host "Release APK built at: build\app\outputs\flutter-apk\app-release.apk"
} finally {
    Pop-Location | Out-Null
}
