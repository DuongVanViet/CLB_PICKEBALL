Param()
Write-Host "Building Flutter debug APK..."
Push-Location -Path "$(Resolve-Path ..)" | Out-Null
try {
    flutter pub get
    flutter build apk --debug
    Write-Host "Debug APK built at: build\app\outputs\flutter-apk\app-debug.apk"
} finally {
    Pop-Location | Out-Null
}
