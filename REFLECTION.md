# Reflection: InventoryHub Full-Stack Integration

## Approach to AI-Assisted Development

While the instructions recommended using Microsoft Copilot to generate integration code, 
I made a deliberate decision to build the project myself first, using Claude (Anthropic) 
as a refinement and verification tool rather than a code generation tool. This decision 
was driven by a belief that understanding the underlying concepts is a prerequisite to 
using AI effectively — without that foundation you cannot evaluate whether what AI 
generates is correct, secure, or appropriate for your use case.

## How AI Assisted the Development Process

### Integration Code
The initial integration code was written independently. Claude was used to verify 
correctness, identify edge cases, and suggest improvements such as separating the 
API call logic into a dedicated `ProductService` rather than keeping it directly 
in the Razor component. This separation of concerns would not have been meaningful 
if I hadn't already understood why it mattered.

### Debugging Issues
CORS configuration and JSON deserialization issues were debugged independently by 
reading error messages and understanding the underlying cause. Claude confirmed the 
diagnosis and helped articulate why `PropertyNameCaseInsensitive = true` was needed 
when dropping down from `GetFromJsonAsync` to manual `JsonSerializer.Deserialize` — 
the convenience method had been handling this silently, and understanding that 
distinction was valuable.

### JSON Structure
The nested JSON structure for product categories was implemented directly following 
the API design requirements. Claude helped articulate industry standard considerations 
such as the distinction between anonymous types used here for simplicity versus proper 
DTOs that would be used in a production environment with a database layer.

### Performance Optimization
Caching strategies were discussed and implemented with Claude's guidance — both 
client-side caching via `_cachedProducts` in `ProductService` to prevent redundant 
API calls, and server-side response caching via `AddResponseCaching` and `CacheOutput()` 
in the Minimal API.

## Challenges Encountered

### CORS
The minimal `Program.cs` provided in the instructions had no CORS configuration, 
which blocked all requests from the Blazor front-end. This was identified independently 
by reading the browser console and understanding the cross-origin request model before 
any AI assistance was used.

### JSON Deserialization Casing
Switching from `GetFromJsonAsync` to manual deserialization introduced a casing 
mismatch between the API's PascalCase response and the default case-sensitive 
`JsonSerializer`. Understanding why this happened — rather than just applying the fix 
— was more valuable than having it generated automatically.

### Hardcoded API URL
The base URL being hardcoded in the component was identified as a maintainability 
issue. While a full fix via `BaseAddress` configuration was discussed, the more 
complete solution using named or typed `HttpClient` registrations was also covered 
as context for larger projects.

## Known Limitations and Planned Improvements

This project intentionally follows the scope of the course instructions. The following 
improvements are technically within my current skillset from previous modules and would 
be implemented in a production version:

- **Database integration** — replacing hardcoded data with Entity Framework Core and 
a proper database context
- **DTOs and shared models** — moving models to a shared project referenced by both 
front-end and back-end, eliminating duplication
- **Typed HttpClient** — replacing the hardcoded URL with a properly configured typed 
client registered in `Program.cs`
- **Authentication and middleware** — adding JWT or session based authentication, 
with middleware handling auth, logging and request validation
- **Proper CORS policy** — restricting origins to specific domains rather than 
`AllowAnyOrigin` which is inappropriate for production
- **Cache invalidation** — the current caching has no expiry or invalidation strategy, 
which would be required with real data

## What I Learned About Using AI Effectively

The most important insight from this project is that AI is most valuable as a 
**thinking partner and reviewer**, not a code generator. Using it to generate code 
before understanding the concepts produces working code you cannot maintain, debug, 
or extend confidently.

The most productive interactions were ones where I had already formed a view — 
identified the problem, understood the likely cause, had a solution in mind — and 
used AI to pressure test that thinking, fill in gaps, or articulate trade-offs more 
clearly. That is a fundamentally different workflow to prompt-and-paste, and produces 
fundamentally different outcomes in terms of actual learning and code quality.

AI can absolutely be used to generate code effectively — and that is the intended 
workflow for this project. However, I made a deliberate choice to build it manually 
first, because understanding the concepts is what makes AI generation genuinely 
useful. Once you understand what you need and why, you can prompt more precisely, 
evaluate what is generated critically, and identify when something is technically 
correct but not appropriate for your specific context. The goal was never to avoid 
AI, but to reach a point where I can use it with confidence rather than dependency.