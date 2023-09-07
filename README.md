# Chartboost Core Unity SDK

## Minimum Requirements

| Plugin | Version |
| ------ | ------ |
| Chartboost Core SDK | 0.0.0+ |
| Cocoapods | 1.11.3+ |
| iOS | 11.0+ |
| Xcode | 14.1+ |
| Android API | 21+ |
| Unity | 2020.3.37f+ |

## Integration

Chartboost Core Unity SDK is distributed using the public [npm registry](https://www.npmjs.com/search?q=com.chartboost.core) as such it is compatible with the Unity Package Manager (UPM). In order to add the Chartboost Core Unity SDK to your project, just add the following to your Unity Project's ***manifest.json*** file. The scoped registry section is required in order to fetch packages from the NpmJS registry.

```json
  "dependencies": {
    "com.chartboost.core": "0.2.0",
    ...
  },
  "scopedRegistries": [
    {
      "name": "NpmJS",
      "url": "https://registry.npmjs.org",
      "scopes": [
        "com.chartboost"
      ]
    }
  ]
```

## Contributions

We are committed to a fully transparent development process and highly appreciate any contributions. Our team regularly monitors and investigates all submissions for the inclusion in our official adapter releases.

Refer to our [CONTRIBUTING](CONTRIBUTING.md) file for more information on how to contribute.

## License

Refer to our [LICENSE](LICENSE.md) file for more information.